using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using AutoMoqCore;
using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.External;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.DomainService.Commands;
using Devboost.Pagamentos.IoC;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServiceStack;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.DomainService.Commands
{
    public class PagamentoCommandTests
    {
        public PagamentoCommandTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ResolveConverters().BuildServiceProvider();
        }

        [Fact(DisplayName = "ProcessarPagamento")]
        [Trait("PagamentoCommandTests", "Commands Tests")]
        public async Task AddUsingRef_Test()
        {

            var mocker = new AutoMoqer();
            var baseCommandMock = mocker.Create<PagamentoCommand>();


            var param = new AutoFaker<CartaoParam>()
                .RuleFor(fake => fake.NumeroCartao, fake => "4539971947033886")
                .RuleFor(fake => fake.CodSeguranca, fake => "254")
                .RuleFor(fake => fake.DataValidade, fake => fake.Date.Future(2))
                .RuleFor(fake => fake.Tipo, fake => TipoCartaoEnum.Credito)
                .Generate();

            var returnGateway = new GatewayDTO
            {
                IdPedido = param.IdPedido,
                StatusPagamento = StatusPagamentoEnum.Aprovado
            };


            var expectResponse = new List<string>();

            var pagamentoRepoMock = mocker.GetMock<IPagamentoRepository>();
            var gatewayMock = mocker.GetMock<IGatewayExternalService>();

            var deliveryMock = mocker.GetMock<IDeliveryExternalService>();
            pagamentoRepoMock.Setup(r => r.AddUsingRef(It.IsAny<PagamentoEntity>())).Returns(Task.Factory.StartNew(()=> string.Empty)).Verifiable();
           
            pagamentoRepoMock.Setup(r => r.Update(It.IsAny<PagamentoEntity>())).Returns(Task.Factory.StartNew(()=> string.Empty)).Verifiable();
            
            deliveryMock.Setup(r => r.SinalizaStatusPagamento(It.IsAny<GatewayDTO>())).Returns(Task.Factory.StartNew(()=> string.Empty)).Verifiable();
            gatewayMock.Setup(r => r.EfetuaPagamento(It.IsAny<PagamentoEntity>())).ReturnsAsync(returnGateway).Verifiable();



            //When

            var result = await baseCommandMock.ProcessarPagamento(param);

            //Then

            pagamentoRepoMock.Verify(mock => mock.AddUsingRef(It.IsAny<PagamentoEntity>()), Times.Once());
            pagamentoRepoMock.Verify(mock => mock.Update(It.IsAny<PagamentoEntity>()), Times.Once());
           
            deliveryMock.Verify(mock => mock.SinalizaStatusPagamento(It.IsAny<GatewayDTO>()), Times.Once());
            gatewayMock.Verify(mock => mock.EfetuaPagamento(It.IsAny<PagamentoEntity>()), Times.Once());

            var comparison = new CompareLogic();
            
            Assert.True(comparison.Compare(result, expectResponse).AreEqual);

        }


    }
}