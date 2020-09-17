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
using System;
using System.Data;
using Devboost.DroneDelivery.Mongo;
using MongoDB.Driver;
using Devboost.DroneDelivery.Domain.Interfaces.External;
using Devboost.DroneDelivery.External;
using Devboost.DroneDelivery.Domain.VOs;

namespace Devboost.DroneDelivery.UnitTestsBDD
{
    public class DepedencyInjectionTests
    {
        public static IServiceProvider BuildServicesProvider(IConfiguration config, IDbConnection dbConnection)
        {
            var services = new ServiceCollection();

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
            services.AddScoped<IPagamentoExternalContext, PagamentoExternalContext>();

            services.AddSingleton(p => new ExternalConfigVO
            {
                UrlPagamento = config.GetValue<string>("PAGAMENTO_URL")

            });

            services.AddTransient((db) =>
            {
                return dbConnection;
            });

            services.AddTransient((mongo) =>
            {
                var database = config.GetValue<string>("MONGO_DATABASE");
                var host = config.GetValue<string>("MONGO_HOST");
                var port = config.GetValue<string>("MONGO_PORT");
                var connectionString = $"mongodb://{host}:{port}";

                var mongoClient = new MongoClient(connectionString);

                return mongoClient.GetDatabase(database);
            });

            return services.BuildServiceProvider();
        }
    }
}