using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService
{
    public class EntregaCommand : IEntregaCommand
    {
        private readonly IPedidoCommand _pedidoCommand;
        private readonly IDroneCommand _droneCommand;        

        public EntregaCommand(IDroneCommand droneCommand, IPedidoCommand pedidoCommand)
        {
            _droneCommand = droneCommand;
            _pedidoCommand = pedidoCommand;
        }

        public async Task<bool> Inicia()
        {
            try
            {
                await _droneCommand.LiberaDrone();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }

        public async Task IniciaByPedido(DeliveryExternalParam param)
        {
            try
            {
                var pedido = await _pedidoCommand.GetById(param.IdPedido);

                if (pedido != null)
                {
                    pedido.StatusPagamento = param.StatusPagamento;
                    await _droneCommand.LiberaDroneByStatusPagamentoPedido(pedido);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }
    }
}