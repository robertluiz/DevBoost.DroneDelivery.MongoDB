using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Devboost.DroneDelivery.Tests.API
{
    public class DronesRepositoryTests
    {
        [Fact(DisplayName = "GetToken")]
        [Trait("AuthControllerTest", "Controller Tests")]
        public async void GetToken_test()
        {
            //Given(Preparação)
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<AuthController>();

            var faker = AutoFaker.Create();

            var param = faker.Generate<AuthParam>();

            var response = faker.Generate<TokenDTO>();

            response.Authenticated = true;

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Ok(response);

            var service = mocker.GetMock<IAuthService>();
            service.Setup(r => r.GetToken(param)).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.GetToken(param);

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.GetToken(param), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }
    }
}