using AutoBogus;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Params;
using Devboost.DroneDelivery.UnitTestsBDD.Fakes;
using TechTalk.SpecFlow;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsBDD
{
    [Binding]
    public class NaCriacaoDoPedidoSelecionarODroneSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPedidoCommand _pedidoCommand;

        public NaCriacaoDoPedidoSelecionarODroneSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _pedidoCommand = new PedidoCommandFake();
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
        public void QuandoOPedidoForInseridoODroneSelecionadoDeveraAoFinalCorresponderAosCriteriosDefinidos()
        {
            var pesoGramas = _scenarioContext.Get<int>("FirstNumber");
            var distanciaMetros = _scenarioContext.Get<float>("SecondNumber");

            //var newUser = new AutoFaker<UsuarioEntity>()
            //    .RuleFor(fake => fake.Role, fake => RoleEnum.Administrador)
            //    .Generate();

            //var login = newUser.Login;

            //_usuariosRepository.Inserir(newUser);

            var pedido = new AutoFaker<PedidoParam>()
                .RuleFor(fake => fake.Peso, fake => pesoGramas)
                .RuleFor(fake => fake.DistanciaEmMetros, fake => distanciaMetros)
                .RuleFor(fake => fake.Login, fake => "UserTest")
                .Generate();

            var result = _pedidoCommand.InserirPedido(pedido);

            _scenarioContext.Add("FinalResult", result.Result);
        }

        [Then(@"se o pedido foi inserido corretamente a seleção do drone deve ser (.*)")]
        public void EntaoOResultadoDeveraSerVerdade(bool pedidoInserido_e_DroneSelecionado)
        {
            var result = _scenarioContext.Get<bool>("FinalResult");

            Assert.Equal(result, pedidoInserido_e_DroneSelecionado);
        }
    }
}