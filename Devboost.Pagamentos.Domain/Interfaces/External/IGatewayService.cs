using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Entities;

namespace Devboost.Pagamentos.Domain.Interfaces.External
{
    public interface IGatewayService
    {
        Task<GatewayDTO> EfetuaPagamento(PagamentoEntity pagamento);
    }
}