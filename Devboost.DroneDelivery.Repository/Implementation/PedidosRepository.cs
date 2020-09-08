using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Repository.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class PedidosRepository : IPedidosRepository, IDisposable
    {
        private readonly IDbConnection _connection;

        public PedidosRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<PedidoEntity>> GetAll()
        {
            

            var list = await _connection.SelectAsync<Pedido>();

            return list.ConvertTo<List<PedidoEntity>>();

        }

        public async Task<PedidoEntity> GetByID(Guid pedidoId)
        {

            _connection.CreateTableIfNotExists<Pedido>();
            var p = await _connection.SingleByIdAsync<Pedido>(pedidoId);

            return p.ConvertTo<PedidoEntity>();

        }

        public async Task<List<PedidoEntity>> GetByDroneID(Guid droneId)
        {
            
            _connection.CreateTableIfNotExists<Pedido>();
            var p = await _connection.SelectAsync<Pedido>(
                p =>
                    p.DroneId == droneId);

            return p.ConvertTo<List<PedidoEntity>>();
            
        }

        public async Task<List<PedidoEntity>> GetByDroneIDAndStatus(Guid droneId, PedidoStatus status)
        {
            
            _connection.CreateTableIfNotExists<Pedido>();
            var p = await _connection.SelectAsync<Pedido>(
                p =>
                    p.DroneId == droneId
                    && p.Status == status.ToString());

            return p.ConvertTo<List<PedidoEntity>>();

        }

        public async Task<PedidoEntity> GetSingleByDroneID(Guid droneId)
        {
            
            _connection.CreateTableIfNotExists<Pedido>();
            var p = await _connection.SingleAsync<Pedido>(
                p =>
                    p.DroneId == droneId
                    && p.Status == PedidoStatus.EmTransito.ToString());

            return p.ConvertTo<PedidoEntity>();

        }

        public async Task Inserir(PedidoEntity pedido)
        {
            var model = pedido.ConvertTo<Pedido>();
            
            
            _connection.CreateTableIfNotExists<Pedido>();
            await _connection.InsertAsync(model);

        }

        public async Task Atualizar(PedidoEntity pedido)
        {
            var model = pedido.ConvertTo<Pedido>();
            
            
            _connection.CreateTableIfNotExists<Pedido>();
            await _connection.UpdateAsync(model);
         
        }



        protected virtual void Dispose(bool disposing)
        {

            if (disposing)
            {
                _connection?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PedidosRepository()
        {
            Dispose(false);
        }
    }
}