using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Repository.Model;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Repository.Implementation
{
	public class PagamentoRepository : BaseRepository<PagamentoEntity, Pagamento>, IPagamentoRepository
	{
		private readonly IDbConnection _connection;

		public PagamentoRepository(IDbConnection connection)
		{
			_connection = connection;
		}
		public async Task Inserir(PagamentoEntity pagamento)
		{
			try
			{
				var model = pagamento.ConvertTo<Pagamento>();
				//if (_connection.CreateTableIfNotExists<Pagamento>()) 
				//{
				//	_connection.CreateTableIfNotExists<FormaPagamento>();
				//	_connection.CreateTableIfNotExists<Cartao>();
				//}
				_connection.Save(model.FormaPagamento.Cartao);
				_connection.Save(model.FormaPagamento);

				await _connection.SaveAsync(model);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public async Task<PagamentoEntity> GetPagamentoByIdPedido(Guid idPedido)
        {
            _connection.CreateTableIfNotExists<Pagamento>();
            var u = await _connection.SingleAsync<Pagamento>(
                c =>
                    c.IdPedido == idPedido);

           
		  return u.ConvertTo<PagamentoEntity>();
        }
	}
    
}