using AutoBogus;
using Devboost.Pagamentos.Api.Controllers;
using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Params;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.API
{
    public class PagamentoControllerTest
    {
        [Fact(DisplayName = "ProcessarPagamento Sucesso")]
        [Trait("PagamentoControllerTest", "Controller Tests")]
        public async void ProcessarPagamento_Test_Sucesso()
        {
            //Given
            var commandMock = new Mock<IPagamentoCommand>();

            var faker = AutoFaker.Create();

            var param = faker.Generate<CartaoParam>();

            var response = "Pagamento realizado com sucesso!";

            var baseControllerMock = new PagamentoController(commandMock.Object);
            var expectResponse = baseControllerMock.Ok(response);

            commandMock.Setup(r => r.ProcessarPagamento(It.IsAny<CartaoParam>())).ReturnsAsync(new List<string>()).Verifiable();

            //When
            var result = await baseControllerMock.Post(param);

            //Then
            var comparison = new CompareLogic();
            commandMock.Verify(mock => mock.ProcessarPagamento(It.IsAny<CartaoParam>()), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }

        [Fact(DisplayName = "ProcessarPagamento InternalServerError")]
        [Trait("PagamentoControllerTest", "Controller Tests")]
        public async void ProcessarPagamento_Test_InternalServerError()
        {
            //Given
            var commandMock = new Mock<IPagamentoCommand>();

            var faker = AutoFaker.Create();

            var param = faker.Generate<CartaoParam>();

            var baseControllerMock = new PagamentoController(commandMock.Object);
            var expectResponse = baseControllerMock.StatusCode(500);

            commandMock.Setup(r => r.ProcessarPagamento(It.IsAny<CartaoParam>())).ThrowsAsync(new Exception()).Verifiable();

            //When
            var result = await baseControllerMock.Post(param);

            //Then
            var comparison = new CompareLogic();
            commandMock.Verify(mock => mock.ProcessarPagamento(It.IsAny<CartaoParam>()), Times.Once());
            Assert.True(comparison.Compare(((ObjectResult)result).StatusCode, expectResponse.StatusCode).AreEqual);
        }
    }
}