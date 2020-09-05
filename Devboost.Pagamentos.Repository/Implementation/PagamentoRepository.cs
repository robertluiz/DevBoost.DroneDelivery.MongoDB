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
    public class PagamentoRepository : BaseRepository<PagamentoEntity>, IPagamentoRepository
    {
        private readonly IDbConnection _connection;

        public PagamentoRepository(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<PagamentoEntity> RetornoPagamento(Guid idPedido)
        {
            _connection.CreateTableIfNotExists<Pagamento>();
            var u = await _connection.SingleAsync<Pagamento>(
                c =>
                    c.IdPedido == idPedido);

            return u.ConvertTo<PagamentoEntity>();
        }
    }
}