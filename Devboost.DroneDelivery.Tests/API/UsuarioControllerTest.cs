using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Params;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Devboost.DroneDelivery.Tests.API
{
    public class UsuarioControllerTest
    {
        [Fact(DisplayName = "Cadastrar")]
        [Trait("UsuarioControllerTest", "Controller Tests")]
        public async void Cadastrar_test()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<UsuarioController>();

            var faker = AutoFaker.Create();

            var param = faker.Generate<UsuarioParam>();

            var response = true;

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Ok("Usuário cadastrado com sucesso!");

            var service = mocker.GetMock<IUsuarioCommand>();
            service.Setup(r => r.Criar(param)).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.Cadastrar(param);

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.Criar(param), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }

        [Fact]
        public async void Cadastrar_test_BadRequest()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<UsuarioController>();

            var faker = AutoFaker.Create();

            var param = new UsuarioParam() { Nome = "", Senha = ""};

            var response = false;

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.BadRequest("Usuário não cadastrado");

            var service = mocker.GetMock<IUsuarioCommand>();
            service.Setup(r => r.Criar(param)).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.Cadastrar(param);

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.Criar(param), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }

        [Fact]
        public async void Cadastrar_test_InternalServerError()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<UsuarioController>();

            var faker = AutoFaker.Create();

            var param = new UsuarioParam() { Nome = "", Senha = "" };

            var responseTask = new Exception();

            var expectResponse = baseControllerMock.StatusCode(500);

            var service = mocker.GetMock<IUsuarioCommand>();
            service.Setup(r => r.Criar(param)).ThrowsAsync(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.Cadastrar(param);

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.Criar(param), Times.Once());
            Assert.True(((ObjectResult)result).StatusCode == expectResponse.StatusCode);
        }

        [Fact(DisplayName = "GetAll")]
        [Trait("UsuarioControllerTest", "Controller Tests")]
        public async void GetAll_test()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseControllerMock = mocker.Create<UsuarioController>();

            var faker = AutoFaker.Create();

            var response = faker.Generate<List<UsuarioEntity>>();

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Ok(response);

            var service = mocker.GetMock<IUsuarioQuery>();
            service.Setup(r => r.GetAll()).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.GetAll();

            //Then
            var comparison = new CompareLogic();
            service.Verify(mock => mock.GetAll(), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }
    }
}