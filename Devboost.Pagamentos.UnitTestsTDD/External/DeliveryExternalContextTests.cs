using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoBogus;
using AutoMoqCore;
using Confluent.Kafka;
using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.External;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.DomainService.Commands;
using Devboost.Pagamentos.External.Context;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.External
{
    public class DeliveryExternalContextTests
    {
        [Fact(DisplayName = "AtualizaStatusPagamento")]
        [Trait("DeliveryExternalContext", "ExternalContext Tests")]
        public async Task AtualizaStatusPagamento_Test()
        {
            var mocker = new AutoMoqer();
            var baseCommandMock = mocker.Create<DeliveryExternalContext>();



            var deliveryParam = new AutoFaker<DeliveryExternalParam>().Generate();
            var result = new AutoFaker<DeliveryResult<Null,string>>().Generate();

            var deliveryMock = mocker.GetMock<IProducer<Null, string>>();
            

            deliveryMock.Setup(r => r.ProduceAsync(It.IsAny<string>(),It.IsAny<Message<Null,string>>(),It.IsAny<CancellationToken>())).ReturnsAsync(result).Verifiable();
           



            //When

             await baseCommandMock.AtualizaStatusPagamento(deliveryParam);

            //Then

            deliveryMock.Verify(mock => mock.ProduceAsync(It.IsAny<string>(), It.IsAny<Message<Null, string>>(), It.IsAny<CancellationToken>()), Times.Once());
            
        }
    }
}