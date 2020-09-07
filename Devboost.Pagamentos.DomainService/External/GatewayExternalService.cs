using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.External;
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Interfaces.External.Context;
using Devboost.Pagamentos.Domain.Params;
using ServiceStack;

namespace Devboost.Pagamentos.DomainService.External
{
    public class GatewayExternalService : IGatewayExternalService
    {
        private readonly IGatewayExternalContext _gatewayExternalContext;

        public GatewayExternalService(IGatewayExternalContext gatewayExternalContext)
        {
            _gatewayExternalContext = gatewayExternalContext;
        }

        public async Task<GatewayDTO> EfetuaPagamento(PagamentoEntity pagamento)
        {
            var result = await  _gatewayExternalContext.EfetuarPagamentoCartao(pagamento.ConvertTo<GatewayCartaoParam>());
            return new GatewayDTO
            {
                IdPedido = pagamento.IdPedido,
                StatusPagamento = result.StatusPagamento
            };
        }
    }
}