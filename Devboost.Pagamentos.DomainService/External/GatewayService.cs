using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.External;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.DomainService.External
{
    public class GatewayService : IGatewayService
    {
        public Task<GatewayDTO> EfetuaPagamento(PagamentoEntity pagamento)
        {
            throw new System.NotImplementedException();
        }
    }
}