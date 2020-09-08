using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;

namespace Devboost.DroneDelivery.Domain.Interfaces.Commands
{
    public interface IDroneCommand
    {
        Task<DroneEntity> SelecionarDrone(PedidoEntity pedido);
        Task LiberaDrone();
        Task LiberaDroneByStatusPagamentoPedido(PedidoEntity pedido);
        Task AtualizaDrone(DroneEntity drone);
    }
}