using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Interfaces.External;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.DomainService.External
{
    public class DeliveryService : IDeliveryService
    {
        public Task SinalizaStatusPagamento(GatewayDTO statusPagamento)
        {
            throw new System.NotImplementedException();
        }
    }
}