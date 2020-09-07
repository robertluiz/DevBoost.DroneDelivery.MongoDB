using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.DTOs;

namespace Devboost.Pagamentos.Domain.Interfaces.External
{
    public interface IDeliveryService
    {
        Task SinalizaStatusPagamento(GatewayDTO statusPagamento);
    }
}