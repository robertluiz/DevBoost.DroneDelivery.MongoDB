using AutoBogus;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.External;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Params;
using Devboost.DroneDelivery.DomainService.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsBDD.Historias.NaCriacaoDoPedidoSelecionaDrone
{
    [Binding]
    public class NaCriacaoDoPedidoSelecionarODroneSteps
    {
        private readonly ScenarioContext _scenarioContext;        
        private readonly IDbConnection _dbConnection;        
        private readonly IServiceProvider _serviceProvider;

        public NaCriacaoDoPedidoSelecionarODroneSteps(ScenarioContext scenarioContext)
        {
            _dbConnection = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).Open();
            _scenarioContext = scenarioContext;
            _serviceProvider = DepedencyInjectionTests.BuildServicesProvider(new ConfigurationBuilder().AddJsonFile($"appsettingsTests.json", true, true).AddEnvironmentVariables().Build(), _dbConnection);            
        }

        [Given(@"um pedido com peso total de (.*) gramas")]
        public void DadoUmPedidoComPesoTotalDe_Gramas(int pesoGramas)
        {
            _scenarioContext.Add("FirstNumber", pesoGramas);
        }

        [Given(@"com destino de entrega com distância prevista para (.*) metros")]
        public void DadoComDestinoDeEntregaComDistanciaPrevistaPara_Metros(float distanciaMetros)
        {
            _scenarioContext.Add("SecondNumber", distanciaMetros);
        }

        [When(@"o pedido for inserido o drone selecionado deverá ao final corresponder aos critérios definidos")]
        public async Task QuandoOPedidoForInseridoODroneSelecionadoDeveraAoFinalCorresponderAosCriteriosDefinidos()
        {            
            var _droneCommandMock = new Mock<IDroneCommand>();
            var _pedidosRepositoryMock = new Mock<IPedidosRepository>();
            var _usuariosRepositoryMock = new Mock<IUsuariosRepository>();
            var _pagamentoExternalContextMock = new Mock<IPagamentoExternalContext>();

            var pesoGramas = _scenarioContext.Get<int>("FirstNumber");
            var distanciaMetros = _scenarioContext.Get<float>("SecondNumber");

            var newUser = new AutoFaker<UsuarioEntity>()
                .RuleFor(fake => fake.Role, fake => RoleEnum.Administrador)
                .RuleFor(fake => fake.Latitude, fake => -23.592806)
                .RuleFor(fake => fake.Longitude, fake => -46.674925)
                .RuleFor(fake => fake.Login, fake => "LoginTest")
                .Generate();

            var login = newUser.Login;

            await _usuariosRepositoryMock.Object.Inserir(newUser);

            var pedido = new AutoFaker<PedidoParam>()
                .RuleFor(fake => fake.Peso, fake => pesoGramas)                
                .RuleFor(fake => fake.Login, fake => login)
                .Generate();

            _usuariosRepositoryMock.Setup(u => u.GetSingleByLogin(login)).Returns(Task.FromResult(newUser));
            
            var _pedidoCommandMock = new Mock<PedidoCommand>(_droneCommandMock.Object, _pedidosRepositoryMock.Object, _usuariosRepositoryMock.Object, _pagamentoExternalContextMock.Object);
            
            var result = await _pedidoCommandMock.Object.InserirPedido(pedido);
            _scenarioContext.Add("FinalResult", result);
        }

        [Then(@"se o pedido foi inserido corretamente a seleção do drone deve ser (.*)")]
        public void EntaoOResultadoDeveraSerVerdade(bool pedidoInserido_e_DroneSelecionado)
        {
            var result = _scenarioContext.Get<bool>("FinalResult");

            Assert.Equal(result, pedidoInserido_e_DroneSelecionado);
        }
    }
}