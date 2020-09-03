using System.Collections.Generic;
using AutoMoqCore;
using AutoBogus;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Devboost.DroneDelivery.Tests.API
{
    public class DroneControllerTest
    {
        [Fact(DisplayName = "SituacaoDrone")]
        [Trait("DroneControllerTest", "Controller Tests")]
        public async void SituacaoDrone_test()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<DroneController>();

            var faker = AutoFaker.Create();

            var response = faker.Generate<List<ConsultaDronePedidoDTO>>();


            var expectResponse = baseControllerMock.Ok(response);

            var service = mocker.GetMock<IDroneQuery>();
            service.Setup(r => r.ConsultaDrone()).ReturnsAsync(response).Verifiable();

            //When

            var result = await baseControllerMock.SituacaoDrone();

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.ConsultaDrone(), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }

        [Fact]
        public async void SituacaoDrone_test_NotFound()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<DroneController>();

            var faker = AutoFaker.Create();

            var response = new List<ConsultaDronePedidoDTO>();

            var expectResponse = baseControllerMock.NotFound(response);

            var service = mocker.GetMock<IDroneQuery>();
            service.Setup(r => r.ConsultaDrone()).ReturnsAsync(response).Verifiable();

            //When
            var result = await baseControllerMock.SituacaoDrone();

            //Then
            //var comparison = new CompareLogic();
            //service.Verify(mock => mock.ConsultaDrone(), Times.Once());
            Assert.True(((NotFoundResult)result).StatusCode == ((NotFoundObjectResult)expectResponse).StatusCode);
        }

        [Fact]
        public async void SituacaoDrone_test_InternalServerError()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<DroneController>();

            var faker = AutoFaker.Create();

            var response = new Exception();

            var expectResponse = baseControllerMock.StatusCode(500);

            var service = mocker.GetMock<IDroneQuery>();
            service.Setup(r => r.ConsultaDrone()).ThrowsAsync(response).Verifiable();

            //When
            var result = await baseControllerMock.SituacaoDrone();

            //Then
            //var comparison = new CompareLogic();
            //service.Verify(mock => mock.ConsultaDrone(), Times.Once());
            Assert.True(((ObjectResult)result).StatusCode == expectResponse.StatusCode);
        }
    }
}