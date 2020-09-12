using Confluent.Kafka;
using devboost.DroneDelivery.Kafka.Consumer.External;
using devboost.DroneDelivery.Kafka.Consumer.Model;
using devboost.DroneDelivery.Kafka.Consumer.Validators;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace devboost.DroneDelivery.Kafka.Consumer
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
            _logger.Information("Recuperando login e senha");
            string usuario = _configuration["user_login"];
            string senha = _configuration["user_pass"];

            _logger.Information("Testando o consumo de mensagens com Kafka");
            string nomeTopic = _configuration["Kafka_Topic"];
            _logger.Information($"Topic = {nomeTopic}");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using (var consumer = GetConsumerBuilder())
                {
                    consumer.Subscribe(nomeTopic);

                    try
                    {
                        while (true)
                        {
                            var cr = consumer.Consume(cts.Token);
                            string dados = cr.Message.Value;

                            _logger.Information(
                                $"Mensagem lida: {dados}");

                            var pedido = JsonSerializer.Deserialize<Pedido>(dados,
                                new JsonSerializerOptions()
                                {
                                    PropertyNameCaseInsensitive = true
                                });

                            var validationResult = new PedidoValidator().Validate(pedido);
                            if (validationResult.IsValid)
                            {
                                var token = _deliveryExternalControl.Logar(new Auth() { Login = usuario, Senha = senha });
                                _deliveryExternalControl.EnviarPedido(pedido, token);
                                _logger.Information("Ação registrada com sucesso!");
                            }
                            else
                                _logger.Error("Dados inválidos para a Ação");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                        _logger.Warning("Cancelada a execução do Consumer...");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }

        private IConsumer<Ignore, string> GetConsumerBuilder()
        {
            string bootstrapServers = _configuration["Kafka_Broker"];
            _logger.Information($"BootstrapServers = {bootstrapServers}");

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = $"consoleapp-di-redis",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _logger.Information($"GroupId = {config.GroupId}");

            return new ConsumerBuilder<Ignore, string>(config).Build();
        }
    }
}