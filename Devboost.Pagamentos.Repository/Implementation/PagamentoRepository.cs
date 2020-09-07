using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Repository.Model;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Threading.Tasks;
using Devboost.Pagamentos.Repository.Tools;

namespace Devboost.Pagamentos.Repository.Implementation
{
	public class PagamentoRepository : BaseRepository<PagamentoEntity, Pagamento>, IPagamentoRepository
	{
		private readonly IDbConnection _connection;

		public PagamentoRepository(IDbConnection connection) : base(connection)
		{
			_connection = connection;
		}
        public override async Task AddUsingRef(PagamentoEntity pagamento)
		{

            var model = pagamento.ConvertTo<Pagamento>(); 
            _connection.CheckBase();
            
            await _connection.SaveAsync(model.FormaPagamento.Cartao);
            await _connection.SaveAsync(model, references: true);

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