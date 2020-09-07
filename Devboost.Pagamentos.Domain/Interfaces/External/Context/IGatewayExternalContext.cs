using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Params;

namespace Devboost.Pagamentos.Domain.Interfaces.External.Context
{
    public interface IGatewayExternalContext
    {
        Task<GatewayExternalDTO> EfetuarPagamentoCartao(GatewayCartaoParam cartaoParam);

    }
}