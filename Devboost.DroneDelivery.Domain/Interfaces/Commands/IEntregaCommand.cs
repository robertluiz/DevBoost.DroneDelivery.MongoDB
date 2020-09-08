using Devboost.DroneDelivery.Domain.DTOs;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Domain.Interfaces.Commands
{
    public interface IEntregaCommand
    {
        Task<bool> Inicia();
        Task IniciaByPedido(DeliveryExternalParam param);
    }
}