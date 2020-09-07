using Devboost.Pagamentos.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Domain.Interfaces.Repository
{
    public interface IPagamentoRepository : IBaseRepository<PagamentoEntity>
    {
		Task<PagamentoEntity> GetPagamentoByIdPedido(Guid idPedido);
    }
}
