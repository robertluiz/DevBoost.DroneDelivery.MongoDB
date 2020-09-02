using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Ok(response);

            var service = mocker.GetMock<IDroneQuery>();
            service.Setup(r => r.ConsultaDrone()).Returns(responseTask).Verifiable();

            //When

            var result = await baseControllerMock.SituacaoDrone();

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.ConsultaDrone(), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }
    }
}