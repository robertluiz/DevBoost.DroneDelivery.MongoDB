using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Params;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.UnitTestsBDD.Fakes
{
    public class PedidoCommandFake : IPedidoCommand
    {
        public Task AtualizaPedido(PedidoEntity pedido)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> InserirPedido(PedidoParam Pedido)
        {
            return Task.FromResult(true);
        }
    }
}