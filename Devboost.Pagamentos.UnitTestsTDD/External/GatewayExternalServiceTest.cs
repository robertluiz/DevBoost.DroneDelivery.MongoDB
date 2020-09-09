using System.Threading.Tasks;
using AutoBogus;
using AutoMoqCore;
using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.External.Context;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.DomainService.External;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.External
{
    public class GatewayExternalServiceTest
    {
        [Fact(DisplayName = "EfetuaPagamento")]
        [Trait("GatewayExternalService", "ExternalService Tests")]
        public async Task EfetuaPagamento_Test()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseExternalMock = mocker.Create<GatewayExternalService>();


            var Cartao = new AutoFaker<CartaoEntity>()
                .RuleFor(fake => fake.Bandeira, fake => PagamentoBandeiraEnum.MasterCard)
                .RuleFor(fake => fake.DataValidade, fake => fake.Date.Future(2))
                .RuleFor(fake => fake.Tipo, fake => TipoCartaoEnum.Credito)
                .Generate();

            var param = new AutoFaker<PagamentoEntity>()
                .RuleFor(fake => fake.StatusPagamento, fake => fake.PickRandom<StatusPagamentoEnum>())
                .RuleFor(fake => fake.Cartao, fake => Cartao)
                .Generate();

            var expectResponse = new GatewayDTO
            {
                IdPedido = param.IdPedido,
                StatusPagamento = StatusPagamentoEnum.Aprovado
            };

            var response = new GatewayExternalDTO
            {
                StatusPagamento = StatusPagamentoEnum.Aprovado
            };
    

            var externalContext = mocker.GetMock<IGatewayExternalContext>();
            externalContext.Setup(e => e.EfetuarPagamentoCartao(It.IsAny<GatewayCartaoParam>())).ReturnsAsync(response).Verifiable();

            //When
            var result = await baseExternalMock.EfetuaPagamento(param);

            //Then
            var comparison = new CompareLogic();
            externalContext.Verify(mock => mock.EfetuarPagamentoCartao(It.IsAny<GatewayCartaoParam>()), Times.Once());
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);

        }
    }
}