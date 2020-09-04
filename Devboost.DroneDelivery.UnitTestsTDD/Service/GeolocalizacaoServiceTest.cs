using Devboost.DroneDelivery.DomainService;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsTDD.Service
{
    public class GeolocalizacaoServiceTest
    {
        [Theory(DisplayName = "CalcularDistanciaEmMetro")]
        [InlineData(-22.8822552,-42.0344928, 479117.37990589684)]
        [InlineData(-23.5874036,-46.6685017, 1234.5114495733537)]
        [InlineData(-23.5666738,-46.636013, 3162.5629158430129)]
        [Trait("GeolocalizacaoService", "Service Tests")]
        public void CalcularDistanciaEmMetro_Theory(double latitudeFinal, double longitudeFinal, double expected)
        {
            var result = GeolocalizacaoService.CalcularDistanciaEmMetro(latitudeFinal, longitudeFinal);
            Assert.Equal(expected, result);
        }
    }
}