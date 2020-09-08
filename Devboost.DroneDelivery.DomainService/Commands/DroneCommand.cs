using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;

namespace Devboost.DroneDelivery.DomainService.Commands
{
    public class DroneCommand : IDroneCommand
    {
        private readonly IDronesRepository _dronesRepository;
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IDroneQuery _droneQuery;

        public DroneCommand(IDronesRepository dronesRepository, IPedidosRepository pedidosRepository, IDroneQuery droneQuery)
        {
            _dronesRepository = dronesRepository;
            _pedidosRepository = pedidosRepository;
            _droneQuery = droneQuery;
        }

        public async Task<DroneEntity> SelecionarDrone(PedidoEntity pedido)
        {
            DroneEntity drone = null;

            var listaDrones = await _dronesRepository.GetAll();

            foreach (var item in listaDrones)
            {
                await AtualizaStatusDrones(item); //Aproveito para atualizar o status de cada drone

                if (!Disponivel(item)) //Se o drone não encontra-se disponível, então pula para verificar a disponibilidade do próximo
                    continue;

                var listPedidosDrone = await _droneQuery.RetornaConsultaDronePedido(item);

                if (SuportaPeso(listPedidosDrone, item, pedido) && TemAutonomiaSuficiente(listPedidosDrone, item, pedido)) 
                {
                    drone = item;
                    break;
                }
            }
            return drone;
        }

        public async Task LiberaDrone()
        {

            var listaDrones = await _dronesRepository.GetByStatus(DroneStatus.Pronto.ToString());

            foreach (var item in listaDrones)
            {
                var listPedidosDrone = await _droneQuery.RetornaConsultaDronePedido(item);

                if (!TemPedido(listPedidosDrone)) //Se o drone ainda não tem pedido, então ele não será atualizado o seu status, pois não tem entrega para efetuar
                {
                    continue;
                }

                if (listPedidosDrone != null)
                {
                    foreach (var p in listPedidosDrone.Pedidos)
                    {
                        p.Status = PedidoStatus.EmTransito.ToString();
                        await _pedidosRepository.Atualizar(p);
                    }
                }

                item.Status = DroneStatus.EmTransito;
                item.DataAtualizacao = DateTime.Now;
                await AtualizaDrone(item);
            }
        }

        public async Task LiberaDroneByStatusPagamentoPedido(PedidoEntity pedido)
        {

            var d = await _dronesRepository.GetByID(pedido.DroneId);
            var listpedidos = await _pedidosRepository.GetByDroneID(d.Id);

            var temPedidoComStatusPagamentoDiferenteDeAprov = listpedidos.Exists(e => e.StatusPagamento != StatusPagamentoEnum.Aprovado && e.Id != pedido.Id);

            if (!temPedidoComStatusPagamentoDiferenteDeAprov) //Se não tem  nenhum pedido vinculado ao Drone com seu status pagamento pendente de APROVADO, então não posso liberar o Drone
            {
                pedido.Status = PedidoStatus.EmTransito.ToString();                
                await _pedidosRepository.Atualizar(pedido);                

                d.Status = DroneStatus.EmTransito;
                d.DataAtualizacao = DateTime.Now;
                await AtualizaDrone(d);
            }
        }


        public async Task AtualizaDrone(DroneEntity drone)
        {
            if (drone == null)
                return;

            await _dronesRepository.Atualizar(drone);
        }

        private void AtualizaStatusDrones(List<DroneEntity> lista)
        {
            lista.ForEach(async (d) => await AtualizaStatusDrones(d));
        }
        private async Task AtualizaStatusDrones(DroneEntity drone)
        {
            drone.DataAtualizacao ??= DateTime.Now;
            var total = (DateTime.Now - drone.DataAtualizacao ).Value.TotalMinutes;

            switch (drone.Status)
            {
                case DroneStatus.Pronto:
                    break;
                case DroneStatus.EmTransito:
                    var pedidos = await _pedidosRepository.GetByDroneID(drone.Id);
                    if (total > drone.AUTONOMIA_RECARGA)
                    {
                        drone.Status = DroneStatus.Pronto;
                        drone.DataAtualizacao = DateTime.Now;
                        await _dronesRepository.Atualizar(drone);
                        foreach (var pedido in pedidos)
                        {
                            pedido.Status = PedidoStatus.Entregue.ToString();
                            await _pedidosRepository.Atualizar(pedido);
                        }

                    }

                    if (total > drone.AUTONOMIA_MAXIMA)
                    {
                        drone.Status = DroneStatus.Carregando;
                        drone.DataAtualizacao = DateTime.Now;
                        await _dronesRepository.Atualizar(drone);
                        foreach (var pedido in pedidos)
                        {
                            pedido.Status = PedidoStatus.Entregue.ToString();
                            await _pedidosRepository.Atualizar(pedido);
                        }
                    }

                    break;
                case DroneStatus.Carregando:
                    if (total > drone.TEMPO_RECARGA_MINUTOS)
                    {
                        drone.Status = DroneStatus.Pronto;
                        drone.DataAtualizacao = DateTime.Now;
                        await _dronesRepository.Atualizar(drone);
                    }
                    break;
                default:
                    drone.Status = DroneStatus.Pronto;
                    drone.DataAtualizacao = DateTime.Now;
                    await _dronesRepository.Atualizar(drone);
                    break;
            }
        }

        #region methods private
        private bool Disponivel(DroneEntity drone)
        {
            return drone.Status.Equals(DroneStatus.Pronto);
        }

        private bool TemPedido(ConsultaDronePedidoDTO listPedidosDrone)
        {
            if (listPedidosDrone.Pedidos == null || listPedidosDrone.Pedidos.Count().Equals(0)) //Se o drone não tem pedidos relacionados, então já pego ele e saio do foreach
            {
                return false;
            }
            return true;
        }

        private bool SuportaPeso(ConsultaDronePedidoDTO listDronePedidos, DroneEntity drone, PedidoEntity pedidoNovo)
        {
            bool suporta = false;

            var pesoDeTodosPedidos = listDronePedidos != null && listDronePedidos.Pedidos != null && listDronePedidos.Pedidos.Count > 0 ? (listDronePedidos.Pedidos.Sum(s => s.Peso) + pedidoNovo.Peso) : pedidoNovo.Peso;

            if (pesoDeTodosPedidos <= drone.Capacidade)
            {
                suporta = true;
            }

            return suporta;
        }

        private bool TemAutonomiaSuficiente(ConsultaDronePedidoDTO listDronePedidos, DroneEntity drone, PedidoEntity pedidoNovo)
        {
            bool temAuto = false;

            var distanciaDeTodosPedidosEmMetros = listDronePedidos != null && listDronePedidos.Pedidos != null && listDronePedidos.Pedidos.Count > 0 ? (listDronePedidos.Pedidos.Sum(s => s.DistanciaDaEntrega) + pedidoNovo.DistanciaDaEntrega) : pedidoNovo.DistanciaDaEntrega;
            var distanciaDeTodosPedidosKM = distanciaDeTodosPedidosEmMetros / 1000;

            var tempoTotalDeViagemParaTodosPedidosEmMinutos = (distanciaDeTodosPedidosKM * 60) / drone.Velocidade; //Multiplicando o KM por 60 minutos para representar Km/h e depois divido pela velocidade máxima do drone
            
            if ((tempoTotalDeViagemParaTodosPedidosEmMinutos * 2) <= drone.Autonomia) // Multiplico por 2 porque tem a ida e a volta, assim saberei o tempo total gasto em Minutos para entregar os pedidos
            {
                temAuto = true;
            }

            return temAuto;

        }
        #endregion
    }
}