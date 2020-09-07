using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Interfaces.External;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.DomainService.Commands;
using Devboost.Pagamentos.DomainService.External;
using Devboost.Pagamentos.Repository.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.OrmLite;
using System.Diagnostics.CodeAnalysis;

namespace Devboost.Pagamentos.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<IPagamentoCommand, PagamentoCommand>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IGatewayService, GatewayService>();
            services.AddScoped<IDeliveryService, DeliveryService>();

            services.AddTransient((db) =>
            {
                var cn = config.GetConnectionString("DroneDelivery");
                var connection = new OrmLiteConnectionFactory(cn,
                    SqlServerDialect.Provider);
                return connection.OpenDbConnection();
            });

            return services;
        }
    }
}