using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Params;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Devboost.DroneDelivery.Tests.API
{
    public class PedidoControllerTest
    {

        [Fact(DisplayName = "Cadastrar")]
        [Trait("PedidoControllerTest", "Controller Tests")]
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

        [Fact(DisplayName = "GetAll")]
        [Trait("PedidoControllerTest", "Controller Tests")]
        public async void GetAll_test()
        {
            //Given
            var queryMock = new Mock<IPedidoQuery>();
            var commandMock = new Mock<IPedidoCommand>();

            var mocker = new AutoMoqer();
            var baseControllerMock = new PedidoController(commandMock.Object, queryMock.Object, "User");

            var faker = AutoFaker.Create();

            var response = faker.Generate<List<PedidoEntity>>();

            var expectResponse = baseControllerMock.Ok(response);

            queryMock.Setup(r => r.GetAll()).ReturnsAsync(response).Verifiable();

            //When
            var result = await baseControllerMock.GetAll();

            //Then
            var comparison = new CompareLogic();
            queryMock.Verify(mock => mock.GetAll(), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }
    }
}