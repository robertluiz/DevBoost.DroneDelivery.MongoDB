using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.DTOs;

namespace Devboost.Pagamentos.Domain.Interfaces.External
{
    public interface IDeliveryExternalService
    {
        Task SinalizaStatusPagamento(GatewayDTO statusPagamento);
    }
}