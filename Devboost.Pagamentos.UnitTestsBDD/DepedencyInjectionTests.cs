using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Interfaces.External;
using Devboost.Pagamentos.Domain.Interfaces.External.Context;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.Domain.VO;
using Devboost.Pagamentos.DomainService.Commands;
using Devboost.Pagamentos.DomainService.External;
using Devboost.Pagamentos.External.Context;
using Devboost.Pagamentos.IoC;
using Devboost.Pagamentos.Repository.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AutoBogus;
using Confluent.Kafka;

namespace Devboost.Pagamentos.UnitTestsBDD
{
    public class DepedencyInjectionTests
    {
        public static IServiceProvider BuildServicesProvider(IConfiguration config, IDbConnection dbConnection)
        {
            var services = new ServiceCollection();

            var kConfig = new ProducerConfig
            {
                BootstrapServers = config.GetValue<string>("ServerKafka")
            };

            var externalConfig = new ExternalConfigVO
            {
                GatewayUrl = config.GetValue<string>("GATEWAY__URL"),
                DeliveryUrl = config.GetValue<string>("DELIVERY_URL"),
                TopicPagamento = config.GetValue<string>("PAGAMENTO_TOPIC")
            };

            services.AddSingleton(p => externalConfig);

            services.AddSingleton(config);
            services.AddScoped<IPagamentoCommand, PagamentoCommand>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            services.AddScoped<IGatewayExternalService, GatewayExternalService>();
            services.AddScoped<IDeliveryExternalService, DeliveryExternalService>();

            services.AddScoped<IGatewayExternalContext, GatewayExternalContext>();
            services.AddScoped<IDeliveryExternalContext, DeliveryExternalContext>();

            services.ResolveConverters();

            //services.AddScoped<IDeliveryExternalContext>((idel) =>
            //{
            //    var mockDeliveryExternalContext = new Mock<DeliveryExternalContext>(externalConfig, kConfig);
            //    mockDeliveryExternalContext.Setup(s => s.AtualizaStatusPagamento(It.IsAny<DeliveryExternalParam>()))
            //        .Verifiable();

            //    return mockDeliveryExternalContext.Object;
            //});


            services.AddScoped((kf) =>
            {
                var result = new AutoFaker<DeliveryResult<Null, string>>().Generate();
                var kafkaMock = new Mock<IProducer<Null, string>>();
                kafkaMock.Setup(r => r.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<Null, string>>(),
                    It.IsAny<CancellationToken>())).ReturnsAsync(result);
                return kafkaMock.Object;
            });

        services.AddTransient((db) =>
            {
                return dbConnection;
            });

            return services.BuildServiceProvider();
        }
    }
}