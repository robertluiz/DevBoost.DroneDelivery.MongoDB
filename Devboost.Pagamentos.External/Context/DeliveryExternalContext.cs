using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Interfaces.External.Context;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.Domain.VO;
using Flurl.Http;

namespace Devboost.Pagamentos.External.Context
{
    public class DeliveryExternalContext: IDeliveryExternalContext
    {
        private readonly ExternalConfigVO _config;

        public DeliveryExternalContext(ExternalConfigVO config)
        {
            _config = config;
        }

        public async Task AtualizaStatusPagamento(DeliveryExternalParam deliverypParam)
        {
            var url = $"{_config.DeliveryUrl}/entrega/inicia/pedido";
            await url.PostJsonAsync(deliverypParam)
                .ConfigureAwait(false);
        }
    }
}