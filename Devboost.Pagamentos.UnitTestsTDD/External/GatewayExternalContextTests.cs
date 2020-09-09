using System.Net.Http;
using System.Threading.Tasks;
using AutoBogus;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.Domain.VO;
using Flurl.Http.Testing;
using Devboost.Pagamentos.External.Context;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.External
{
    public class GatewayExternalContextTests
    {
        [Fact(DisplayName = "EfetuarPagamentoCartao")]
        [Trait("GatewayExternalContext", "ExternalContext Tests")]
        public async Task EfetuarPagamentoCartao_Test()
        {
            var baseExternalMock = new GatewayExternalContext( new ExternalConfigVO
            {
                DeliveryUrl = "http://teste",
                GatewayUrl = "http://teste"
            });

            var param = new AutoFaker<GatewayCartaoParam>().Generate();

            using var httpTest = new HttpTest();
            // arrange
            httpTest.RespondWithJson(new { StatusPagamentoEnum = "Aprovado" });

            // act
            await baseExternalMock.EfetuarPagamentoCartao(param);

            // assert
            httpTest.ShouldHaveCalled("http://teste/payments")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(param)
                .Times(1);
        }
    }
}