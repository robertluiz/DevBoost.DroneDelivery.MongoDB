using System;
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.External.Context;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.Domain.VO;
using Flurl.Http;

namespace Devboost.Pagamentos.External.Context
{
    public class GatewayExternalContext:IGatewayExternalContext
    {
        private readonly ExternalConfigVO _config;

        public GatewayExternalContext(ExternalConfigVO config)
        {
            _config = config;
        }

        public async Task<GatewayExternalDTO> EfetuarPagamentoCartao(GatewayCartaoParam cartaoParam)
        {
            var url = $"{_config.GatewayUrl}/payments";

            try
            {
                return await url.PostJsonAsync(cartaoParam)
                    .ReceiveJson<GatewayExternalDTO>()
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new GatewayExternalDTO
                {
                    StatusPagamento = StatusPagamentoEnum.Recusado
                };
            }


        }
    }
}