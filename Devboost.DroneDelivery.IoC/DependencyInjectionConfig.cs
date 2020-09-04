using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.DomainService;
using Devboost.DroneDelivery.DomainService.Commands;
using Devboost.DroneDelivery.DomainService.Queries;
using Devboost.DroneDelivery.Repository.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.OrmLite;
using System.Diagnostics.CodeAnalysis;

namespace Devboost.DroneDelivery.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<IDronesRepository, DronesRepository>();
            services.AddScoped<IPedidosRepository, PedidosRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEntregaCommand, EntregaCommand>();
            services.AddScoped<IDroneCommand, DroneCommand>();
            services.AddScoped<IDroneQuery, DroneQuery>();
            services.AddScoped<IPedidoCommand, PedidoCommand>();
            services.AddScoped<IPedidoQuery, PedidoQuery>();
            services.AddScoped<IUsuarioCommand, UsuarioCommand>();
            services.AddScoped<IUsuarioQuery, UsuarioQuery>();            
            services.AddTransient( (db) =>
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