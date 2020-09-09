using AutoBogus;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Params;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsBDD.Historias.ProcessarPagamento
{
    [Binding]
    public class ProcessarPagamentoSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPagamentoCommand _pagamentoCommand;
        private readonly IDbConnection _dbConnection;
        private readonly IServiceProvider _serviceProvider;

        public ProcessarPagamentoSteps(ScenarioContext scenarioContext)
        {
            _dbConnection = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).Open();
            _scenarioContext = scenarioContext;
            _serviceProvider = _serviceProvider = DepedencyInjectionTests.BuildServicesProvider(new ConfigurationBuilder().AddJsonFile($"appsettingsTests.json", true, true).AddEnvironmentVariables().Build(), _dbConnection);
            _pagamentoCommand = _serviceProvider.GetRequiredService<IPagamentoCommand>();
        }

        [Given(@"o número do cartao (.*)")]
        public void DadoONumeroDoCartao(string numeroCartao)
        {
            _scenarioContext.Add("FirstValue", numeroCartao);
        }

        [Given(@"nome (.*)")]
        public void DadoNomeJonathanMenezes(string nome)
        {
            _scenarioContext.Add("SecondValue", nome);
        }

        [Given(@"código de segurança (.*)")]
        public void DadoCodigoDeSeguranca(string codSeguranca)
        {
            _scenarioContext.Add("ThirdValue", codSeguranca);
        }

        [When(@"os dados forem enviados para processamento")]
        public async Task QuandoOsDadosForemEnviadosParaProcessamento()
        {
            var numero = _scenarioContext.Get<string>("FirstValue");
            var nome = _scenarioContext.Get<string>("SecondValue");
            var codSeg = _scenarioContext.Get<string>("ThirdValue");

            var cartao = new AutoFaker<CartaoParam>()
            .RuleFor(fake => fake.NumeroCartao, fake => numero)
            .RuleFor(fake => fake.Nome, fake => nome)
            .RuleFor(fake => fake.CodSeguranca, fake => codSeg)
            .RuleFor(fake => fake.DataValidade, fake => DateTime.Now.AddYears(2))
            .RuleFor(fake => fake.Bandeira, fake => PagamentoBandeiraEnum.MasterCard)
            .Generate();

            var listResult = await _pagamentoCommand.ProcessarPagamento(cartao);
            _scenarioContext.Add("FinalResult", listResult);
        }

        [Then(@"o resultado deve retornar uma lista sem erros")]
        public void EntaoOResultadoDeveraSerAprovado()
        {
            List<string> listResultExpected = new List<string>();

            var result = _scenarioContext.Get<List<string>>("FinalResult");
            Assert.Equal(result, listResultExpected);
        }
    }
}