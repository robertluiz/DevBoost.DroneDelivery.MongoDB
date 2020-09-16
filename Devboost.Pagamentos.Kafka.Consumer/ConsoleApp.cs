using System;
using System.Text.Json;
using System.Threading;
using Confluent.Kafka;
using devboost.DroneDelivery.Kafka.Consumer.Utils;
using Devboost.Pagamentos.Kafka.Consumer.External;
using Devboost.Pagamentos.Kafka.Consumer.Model;
using Devboost.Pagamentos.Kafka.Consumer.Validators;
using Microsoft.Extensions.Configuration;
using Serilog.Core;

namespace Devboost.Pagamentos.Kafka.Consumer
{
    public class ConsoleApp
    {
        private readonly Logger _logger;
        private readonly IConfiguration _configuration;
        private readonly DeliveryExternalControl _deliveryExternalControl;

        public ConsoleApp(Logger logger, IConfiguration configuration, DeliveryExternalControl deliveryExternalControl)
        {
            _logger = logger;
            _configuration = configuration;
            _deliveryExternalControl = deliveryExternalControl;
        }

        public void Run()
        {

            _logger.Information("Testando o consumo de mensagens com Kafka");
            var nomeTopic = _configuration["Kafka_Topic"];
            _logger.Information($"Topic = {nomeTopic}");
            try
            {
                var bootstrapServers = _configuration["Kafka_Broker"];
                TopicTools.CreateTopicAsync(bootstrapServers, nomeTopic);
            }
            catch (Exception e)
            {
                _logger.Warning("Topic já existe");

            }
            

            try
            {
                using var consumer = GetConsumerBuilder();
                consumer.Subscribe(nomeTopic);

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume();
                        var dados = cr.Message.Value;

                        _logger.Information(
                            $"Mensagem lida: {dados}");

                        var pagamento = JsonSerializer.Deserialize<PagamentoStatus>(dados,
                            new JsonSerializerOptions()
                            {
                                PropertyNameCaseInsensitive = true
                            });

                        var validationResult = new PagamentoStatusValidator().Validate(pagamento);
                        if (validationResult.IsValid)
                        {
                                
                            _deliveryExternalControl.AtualizaStatusPagamento(pagamento);
                            _logger.Information("Ação registrada com sucesso!");
                        }
                        else
                            _logger.Warning("Dados inválidos para a Ação");
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                    _logger.Warning("Cancelada a execução do Consumer...");
                }
            }
            catch (Exception ex)
            {
                _logger.Warning($"Exceção: {ex.GetType().FullName} | " +
                                $"Mensagem: {ex.Message}");
            }
        }

        private IConsumer<Ignore, string> GetConsumerBuilder()
        {
            var bootstrapServers = _configuration["Kafka_Broker"];
            _logger.Information($"BootstrapServers = {bootstrapServers}");
            
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = $"pagamento-consumer",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _logger.Information($"GroupId = {config.GroupId}");

            return new ConsumerBuilder<Ignore, string>(config).Build();
        }
    }
}