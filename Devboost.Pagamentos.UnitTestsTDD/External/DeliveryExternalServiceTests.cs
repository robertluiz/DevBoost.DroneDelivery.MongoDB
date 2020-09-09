using System;
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
    public class DeliveryExternalServiceTests
    {
        [Fact(DisplayName = "SinalizaStatusPagamento")]
        [Trait("DeliveryExternalService", "ExternalService Tests")]
        public async Task SinalizaStatusPagamento_Test()
        {
            //Given
            var mocker = new AutoMoqer();
            var baseExternalMock = mocker.Create<DeliveryExternalService>();


            var param = new GatewayDTO
            {
                IdPedido = Guid.NewGuid(),
                StatusPagamento = StatusPagamentoEnum.Aprovado
            };

            var response = Task.Factory.StartNew(() => string.Empty);

            

            var externalContext = mocker.GetMock<IDeliveryExternalContext>();
            externalContext.Setup(e => e.AtualizaStatusPagamento(It.IsAny<DeliveryExternalParam>())).Returns(response).Verifiable();

            //When
             await baseExternalMock.SinalizaStatusPagamento(param);

            //Then
            var comparison = new CompareLogic();
            externalContext.Verify(mock => mock.AtualizaStatusPagamento(It.IsAny<DeliveryExternalParam>()), Times.Once());
            

        }
    }
}