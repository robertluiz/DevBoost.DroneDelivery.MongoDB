using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
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
    }
}