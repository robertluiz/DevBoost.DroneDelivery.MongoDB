using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService.Queries
{
    public class DroneQuery : IDroneQuery
    {
        private readonly IDronesRepository _dronesRepository;
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IUsuariosRepository _usuariosRepository;

        public DroneQuery(IDronesRepository dronesRepository, IPedidosRepository pedidosRepository, IUsuariosRepository usuariosRepository)
        {
            _dronesRepository = dronesRepository;
            _pedidosRepository = pedidosRepository;
            _usuariosRepository = usuariosRepository;
        }

        public async Task<List<ConsultaDronePedidoDTO>> ConsultaDrone()
        {
           // var listaDrones = await _dronesRepository.GetAll();
            //AtualizaStatusDrones(listaDrones); Por enquanto os Drones nao serão atualizados via consulta de pedido, talvez um fila será responável por fazer essa atualização
            var drones = await _dronesRepository.GetAll();
            return drones.Select(async d => await RetornaConsultaDronePedido(d))
                .ToList()
                .Select(c => c.Result)
                .ToList();

        }

        public async Task<ConsultaDronePedidoDTO> RetornaConsultaDronePedido(DroneEntity drone)
        {

            var pedidos = await _pedidosRepository.GetByDroneID(drone.Id);

            List<ConsultaPedidoCompradorDTO> listPedidoComprador = null;

            if (pedidos != null)
            {
                listPedidoComprador = pedidos.Select(async d => await GetPedidosWithComprador(d))
                .Select(c => c.Result).ToList();

            }

            return new ConsultaDronePedidoDTO
            {

                IdDrone = drone.Id,
                Situacao = drone.Status.ToString(),
                Pedidos = pedidos,
                PedidosComprador = listPedidoComprador
            };
        }

        public async Task<ConsultaPedidoCompradorDTO> GetPedidosWithComprador(PedidoEntity p)
        {
            var userComprador = await _usuariosRepository.GetSingleById(p.CompradorId);

            return new ConsultaPedidoCompradorDTO
            {
                PedidoId = p.Id,
                Status = p.Status,
                CompradorId = p.CompradorId,
                NomeComprador = userComprador.Nome,
                DataHora = p.DataHora,
                DistanciaDaEntrega = p.DistanciaDaEntrega,
                Peso = p.Peso,
                Latitude = userComprador.Latitude,
                Longitude = userComprador.Longitude
            };
        }
    }
}