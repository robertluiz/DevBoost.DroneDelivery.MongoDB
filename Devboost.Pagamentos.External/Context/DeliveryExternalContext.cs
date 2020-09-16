using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Devboost.Pagamentos.Domain.Interfaces.External.Context;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.Domain.VO;

namespace Devboost.Pagamentos.External.Context
{
    public class DeliveryExternalContext: IDeliveryExternalContext, IDisposable
    {
        private readonly ExternalConfigVO _config;
        private readonly IProducer<Null, string> _producer;

        public DeliveryExternalContext(ExternalConfigVO config, IProducer<Null, string> producer)
        {
            _config = config;
            _producer = producer;
        }

        public virtual async Task AtualizaStatusPagamento(DeliveryExternalParam deliverypParam)
        {
            var deliveryJson = JsonSerializer.Serialize(deliverypParam);
            await _producer.ProduceAsync(_config.TopicPagamento,
                new Message<Null, string>
                {
                    Value = deliveryJson
                });
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}