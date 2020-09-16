using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Params;

namespace Devboost.DroneDelivery.Domain.Interfaces.External
{
    public interface IPagamentoExternalContext
    {
        Task EfetuarPagamentoCartao(PagamentoCartaoParam pagamento);
    }
}