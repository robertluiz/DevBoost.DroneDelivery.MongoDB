using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Repository.Implementation
{
    public class PagamentoRepository : IPagamentoRepository
    {
        public Task Inserir(PagamentoEntity pagamento)
        {
            throw new NotImplementedException();
        }

        public Task<PagamentoEntity> RetornoPagamento(Guid idPedido)
        {
            throw new NotImplementedException();
        }
    }
}