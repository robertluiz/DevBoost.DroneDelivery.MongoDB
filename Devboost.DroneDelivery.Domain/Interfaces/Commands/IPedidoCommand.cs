using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Params;

namespace Devboost.DroneDelivery.Domain.Interfaces.Commands
{
    public interface IPedidoCommand
    {
        Task<bool> InserirPedido(PedidoParam Pedido);        
        Task AtualizaPedido(PedidoEntity pedido);
        Task<PedidoEntity> GetById(Guid pedidoId);
    }
}