using AutoBogus;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.UnitTestsTDD.Repository
{
    public class PedidosRepositoryFake: IPedidosRepository
    {
        private readonly List<PedidoEntity> _pedidos;
        private readonly IAutoFaker _autoFaker;

        public PedidosRepositoryFake()
        {
            _autoFaker = AutoFaker.Create();
            _pedidos = _autoFaker.Generate<List<PedidoEntity>>();
        }

        public Task Atualizar(PedidoEntity pedido)
        {
            _pedidos.FirstOrDefault().Status = pedido.Status;            

            return Task.CompletedTask;
        }

        public Task<List<PedidoEntity>> GetAll()
        {
            return Task.FromResult(_pedidos);
        }

        public Task<List<PedidoEntity>> GetByDroneID(Guid droneId)
        {
            return Task.FromResult(_pedidos.Where(s => s.DroneId == droneId).ToList());
        }

        public Task<List<PedidoEntity>> GetByDroneIDAndStatus(Guid droneId, PedidoStatus status)
        {
            return Task.FromResult(_pedidos.Where(s => s.DroneId == droneId && s.Status == status.ToString()).ToList());
        }

        public Task<PedidoEntity> GetSingleByDroneID(Guid droneId)
        {
            return Task.FromResult(_pedidos.Where(s => s.DroneId == droneId).FirstOrDefault());
        }

        public Task Inserir(PedidoEntity pedido)
        {
            _pedidos.Add(pedido);
            return Task.CompletedTask;
        }
    }
}