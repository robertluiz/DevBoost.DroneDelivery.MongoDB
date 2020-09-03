using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Devboost.DroneDelivery.Tests.API
{
    public class EntregaControllerTest
    {
        [Fact(DisplayName = "IniciaEntrega")]
        [Trait("EntregaControllerTest", "Controller Tests")]
        public async void IniciaEntrega_test()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<EntregaController>();

            var faker = AutoFaker.Create();

            var response = true;

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Ok("Entrega iniciada!");

            var service = mocker.GetMock<IEntregaCommand>();
            service.Setup(r => r.Inicia()).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.IniciaEntrega();

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.Inicia(), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }

        [Fact]
        public async void IniciaEntrega_test_BadRequest()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<EntregaController>();

            var faker = AutoFaker.Create();

            var response = false;

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.BadRequest("Entrega iniciada!");

            var service = mocker.GetMock<IEntregaCommand>();
            service.Setup(r => r.Inicia()).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.IniciaEntrega();

            //Then
            //var comparison = new CompareLogic();
            service.Verify(mock => mock.Inicia(), Times.Once());
            Assert.True(((BadRequestResult)result).StatusCode == ((BadRequestObjectResult)expectResponse).StatusCode);
        }

        [Fact]
        public async void IniciaEntrega_test_InternalServerError()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<EntregaController>();

            var faker = AutoFaker.Create();

            var responseTask = new Exception();

            var expectResponse = baseControllerMock.StatusCode(500);

            var service = mocker.GetMock<IEntregaCommand>();
            service.Setup(r => r.Inicia()).ThrowsAsync(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.IniciaEntrega();

            //Then
            //var comparison = new CompareLogic();
            service.Verify(mock => mock.Inicia(), Times.Once());
            Assert.True(((ObjectResult)result).StatusCode == expectResponse.StatusCode);
        }
    }
}