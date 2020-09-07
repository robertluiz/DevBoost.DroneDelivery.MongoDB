using AutoBogus;
using Devboost.Pagamentos.Api.Controllers;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Params;
using KellermanSoftware.CompareNetObjects;
using Moq;
using ServiceStack;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.API
{
    public class PagamentoControllerTest
    {
        [Fact(DisplayName = "ProcessarPagamento")]
        [Trait("PagamentoControllerTest", "Controller Tests")]
        public async void ProcessarPagamento_Test_Sucesso()
        {
            //Given
            var commandMock = new Mock<IPagamentoCommand>();

            var faker = AutoFaker.Create();

            var param = faker.Generate<CartaoParam>();

            var response = "Pedido realizado com sucesso!";

            var baseControllerMock = new PagamentoController(commandMock.Object);
            var expectResponse = baseControllerMock.Ok(response);

            var pagamento = param.ConvertTo<PagamentoEntity>();
            var erros = pagamento.Validar();

            commandMock.Setup(r => r.ProcessarPagamento(It.IsAny<CartaoParam>())).ReturnsAsync(erros).Verifiable();

            //When
            var result = await baseControllerMock.Post(param);

            //Then
            var comparison = new CompareLogic();
            commandMock.Verify(mock => mock.ProcessarPagamento(It.IsAny<CartaoParam>()), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);
        }
    }
}