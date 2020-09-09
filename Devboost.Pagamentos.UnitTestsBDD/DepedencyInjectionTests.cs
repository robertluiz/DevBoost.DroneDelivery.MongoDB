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
using System.Threading.Tasks;

namespace Devboost.Pagamentos.UnitTestsBDD
{
    public class DepedencyInjectionTests
    {
        public static IServiceProvider BuildServicesProvider(IConfiguration config, IDbConnection dbConnection)
        {
            var services = new ServiceCollection();

            var externalConfig = new ExternalConfigVO
            {
                GatewayUrl = config.GetValue<string>("GATEWAY__URL"),
                DeliveryUrl = config.GetValue<string>("DELIVERY_URL")
            };

            services.AddSingleton(p => externalConfig);

            services.AddSingleton(config);
            services.AddScoped<IPagamentoCommand, PagamentoCommand>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            services.AddScoped<IGatewayExternalService, GatewayExternalService>();
            services.AddScoped<IDeliveryExternalService, DeliveryExternalService>();

            services.AddScoped<IGatewayExternalContext, GatewayExternalContext>();

            services.ResolveConverters();

            services.AddScoped<IDeliveryExternalContext>((idel) =>
            {
                var mockDeliveryExternalContext = new Mock<DeliveryExternalContext>(externalConfig);
                mockDeliveryExternalContext.Setup(s => s.AtualizaStatusPagamento(It.IsAny<DeliveryExternalParam>())).Returns(Task.Factory.StartNew(() => string.Empty));

                return mockDeliveryExternalContext.Object;
            });

            services.AddTransient((db) =>
            {
                return dbConnection;
            });

            return services.BuildServiceProvider();
        }
    }
}