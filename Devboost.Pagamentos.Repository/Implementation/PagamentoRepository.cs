using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Devboost.Pagamentos.Repository.Model;

namespace Devboost.Pagamentos.Repository.Implementation
{
	public class PagamentoRepository : IPagamentoRepository
	{
		private readonly IDbConnection _connection;

		public PagamentoRepository(IDbConnection connection)
		{
			_connection = connection;
		}
		public async Task Inserir(PagamentoEntity pagamento)
		{
			var model = pagamento.ConvertTo<Pagamento>();
			_connection.CreateTableIfNotExists<Pagamento>();
			await _connection.SaveAsync(model, references:true);
		}

		public async Task<PagamentoEntity> RetornoPagamento(Guid idPedido)
		{
			_connection.CreateTableIfNotExists<Pagamento>();
			var u = await _connection.SingleAsync<Pagamento>(
				c =>
					c.IdPedido == idPedido);

			return u.ConvertTo<PagamentoEntity>();
		}

		public async Task Atualizar(PagamentoEntity pagamento) 
		{
			var model = pagamento.ConvertTo<Pagamento>();

			_connection.CreateTableIfNotExists<Pagamento>();
			await _connection.UpdateAsync(model);
		}
	}
}
