using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Devboost.DroneDelivery.Tests.API
{
    public class AuthControllerTest
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

        [Fact]
        public async void GetToken_test_Unauthorized()
        {
            //Given(Preparação)
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<AuthController>();

            var faker = AutoFaker.Create();

            var param = new AuthParam() { Login = "TESTE", Senha = "123456" };

            var response = faker.Generate<TokenDTO>();

            response.Authenticated = false;

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Unauthorized(response);

            var service = mocker.GetMock<IAuthService>();
            service.Setup(r => r.GetToken(param)).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.GetToken(param);

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.GetToken(param), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }

        [Fact]
        public async void GetToken_test_InternalServerError()
        {
            //Given(Preparação)
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<AuthController>();

            var faker = AutoFaker.Create();

            var param = new AuthParam() { Login = "TESTE", Senha = "123456" };

            var response = faker.Generate<TokenDTO>();

            response.Authenticated = false;

            var responseTask = new Exception();

            var expectResponse = baseControllerMock.StatusCode(500);

            var service = mocker.GetMock<IAuthService>();
            service.Setup(r => r.GetToken(param)).ThrowsAsync(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.GetToken(param);

            //Then
            //var comparison = new CompareLogic();
            service.Verify(mock => mock.GetToken(param), Times.Once());
            Assert.True(((ObjectResult)result).StatusCode == expectResponse.StatusCode);
        }
    }
}