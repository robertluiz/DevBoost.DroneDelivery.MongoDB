using System;
using Devboost.DroneDelivery.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IPedidosRepository
    {
        Task<List<PedidoEntity>> GetAll();
        Task<PedidoEntity> GetByID(Guid pedidoId);
        Task<List<PedidoEntity>> GetByDroneID(Guid droneId);
        Task<List<PedidoEntity>> GetByDroneIDAndStatus(Guid droneId, PedidoStatus status);
        Task<PedidoEntity> GetSingleByDroneID(Guid droneId);
        Task Inserir(PedidoEntity pedido);
        Task Atualizar(PedidoEntity pedido);

    }
}
