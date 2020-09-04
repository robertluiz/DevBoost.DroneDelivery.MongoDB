using AutoBogus;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Params;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsBDD
{
    [Binding]
    public class CriandoOUsuarioSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IUsuarioCommand _usuarioCommand;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IDbConnection _dbConnection;
        private readonly IServiceProvider _serviceProvider;

        public CriandoOUsuarioSteps(ScenarioContext scenarioContext)
        {
            _dbConnection = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).Open();
            _scenarioContext = scenarioContext;
            //_usuariosRepository = new UsuariosRepository(_dbConnection);
            _serviceProvider = DepedencyInjectionTests.BuildServicesProvider(new ConfigurationBuilder().Build(), _dbConnection);
            _usuarioCommand = _serviceProvider.GetRequiredService<IUsuarioCommand>();
        }

        [Given(@"o usuário (.*)")]
        public void DadoOUsuarioFulanoAlmeida(string login)
        {
            _scenarioContext.Add("FirstNumber", login);
        }

        [Given(@"senha (.*)")]
        public void DadoSenhaABc(string senha)
        {
            _scenarioContext.Add("SecondNumber", senha);
        }

        [When(@"inserido o usuário")]
        public async Task QuandoInseridoOUsuario()
        {
            var login = _scenarioContext.Get<string>("FirstNumber");
            var senha = _scenarioContext.Get<string>("SecondNumber");

            var usuario = new AutoFaker<UsuarioParam>()
                .RuleFor(fake => fake.Login, fake => login)
                .RuleFor(fake => fake.Senha, fake => senha)
                .RuleFor(fake => fake.Role, fake => RoleEnum.Administrador.ToString())
                .Generate();

            var result = await _usuarioCommand.Criar(usuario);
            _scenarioContext.Add("FinalResult", result);
        }

        [Then(@"se for cadastrado com sucesso o resultado será (.*)")]
        public void EntaoSeForCadastradoComSucessoOResultadoSeraTrue(bool respostaCadastroEsperada)
        {
            var result = _scenarioContext.Get<bool>("FinalResult");

            Assert.Equal(result, respostaCadastroEsperada);
        }
    }
}