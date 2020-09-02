using AutoBogus;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Params;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Xunit;

namespace Devboost.DroneDelivery.Tests.API
{
    public class PedidoControllerTest
    {

        [Fact(DisplayName = "Cadastrar")]
        [Trait("PedidoController", "Controller Tests")]
        public async void Cadastrar_test()
        {
            //Given

            var queryMock = new Mock<IPedidoQuery>();
            var commandMock = new Mock<IPedidoCommand>();
            
            var faker = AutoFaker.Create();

            var param = faker.Generate<PedidoParam>();

            var response = "Pedido realizado com sucesso!";

            var baseControllerMock = new PedidoController(commandMock.Object, queryMock.Object, "User");
            var expectResponse = baseControllerMock.Ok(response);


            commandMock.Setup(r => r.InserirPedido(It.IsAny<PedidoParam>())).ReturnsAsync(true).Verifiable();
 
            //When

            var result = await baseControllerMock.Cadastrar(param);

            //Then
            var comparison = new CompareLogic();
            commandMock.Verify(mock => mock.InserirPedido(It.IsAny<PedidoParam>()), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }
    }
}