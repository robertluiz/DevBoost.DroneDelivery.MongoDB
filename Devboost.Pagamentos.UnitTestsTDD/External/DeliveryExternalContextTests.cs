using System.Net.Http;
using System.Threading.Tasks;
using AutoBogus;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.Domain.VO;
using Devboost.Pagamentos.External.Context;
using Flurl.Http.Testing;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.External
{
    public class DeliveryExternalContextTests
    {
        [Fact(DisplayName = "AtualizaStatusPagamento")]
        [Trait("DeliveryExternalContext", "ExternalContext Tests")]
        public async Task AtualizaStatusPagamento_Test()
        {
            var baseExternalMock = new DeliveryExternalContext(new ExternalConfigVO
            {
                DeliveryUrl = "http://teste",
                GatewayUrl = "http://teste"
            });

            var param = new AutoFaker<DeliveryExternalParam>().Generate();

            using var httpTest = new HttpTest();
            // arrange
            httpTest.RespondWith();

            // act
            await baseExternalMock.AtualizaStatusPagamento(param);

            // assert
            httpTest.ShouldHaveCalled("http://teste/entrega/inicia/pedido")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(param)
                .Times(1);
        }
    }
}