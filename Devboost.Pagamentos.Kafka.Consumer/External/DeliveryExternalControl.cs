using System.Threading.Tasks;
using Devboost.Pagamentos.Kafka.Consumer.Model;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Devboost.Pagamentos.Kafka.Consumer.External
{
    public class DeliveryExternalControl
    {
        private readonly string _deliveryURL;

        public DeliveryExternalControl(IConfiguration configuration)
        {
            _deliveryURL = configuration.GetValue<string>("DELIVERY__URL");
        }
        public virtual void AtualizaStatusPagamento(PagamentoStatus deliverypParam)
        {
            var url = $"{_deliveryURL}/entrega/inicia/pedido";
            url.PostJsonAsync(deliverypParam)
                .ConfigureAwait(false);
        }
    }
}