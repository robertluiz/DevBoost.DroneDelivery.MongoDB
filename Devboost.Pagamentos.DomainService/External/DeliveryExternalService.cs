using Devboost.Pagamentos.Domain.DTOs;
using Devboost.Pagamentos.Domain.Interfaces.External;
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Interfaces.External.Context;
using Devboost.Pagamentos.Domain.Params;
using ServiceStack;

namespace Devboost.Pagamentos.DomainService.External
{
    public class DeliveryExternalService : IDeliveryExternalService
    {
        private readonly IDeliveryExternalContext _deliveryExternalContext;

        public DeliveryExternalService(IDeliveryExternalContext deliveryExternalContext)
        {
            _deliveryExternalContext = deliveryExternalContext;
        }

        public async Task SinalizaStatusPagamento(GatewayDTO statusPagamento)
        {
            await _deliveryExternalContext.AtualizaStatusPagamento(statusPagamento.ConvertTo<DeliveryExternalParam>());
        }
    }
}