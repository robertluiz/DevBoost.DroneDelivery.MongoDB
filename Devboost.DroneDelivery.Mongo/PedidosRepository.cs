using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Repository.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;

namespace Devboost.DroneDelivery.Mongo
{
    public class PedidosRepository : IPedidosRepository
    {
        private readonly IMongoCollection<Pedido> _pedidolCollection;
        private FilterDefinitionBuilder<Pedido> _filterDef => Builders<Pedido>.Filter;
        public PedidosRepository(IMongoDatabase database)
        {
            _pedidolCollection = database.GetCollection<Pedido>("Pedido"); ;
        }


        public async Task<List<PedidoEntity>> GetAll()
        {
            var list = await _pedidolCollection.AsQueryable().ToListAsync();

            return list.ConvertTo<List<PedidoEntity>>();
        }

        public async Task<PedidoEntity> GetByID(Guid pedidoId)
        {
            var bsonId = new BsonBinaryData(pedidoId);

            var filter = _filterDef.Eq(_ => _.Id, bsonId);

            var p = (await _pedidolCollection.FindAsync(filter)).FirstOrDefault();

            return p.ConvertTo<PedidoEntity>();

        }

        public async Task<List<PedidoEntity>> GetByDroneID(Guid droneId)
        {
            var bsonId = new BsonBinaryData(droneId);

            var filter = _filterDef.Eq(_ => _.DroneId, bsonId);

            var p = (await _pedidolCollection.FindAsync(filter)).FirstOrDefault();

            return p.ConvertTo<List<PedidoEntity>>();

        }

        public async Task<List<PedidoEntity>> GetByDroneIDAndStatus(Guid droneId, PedidoStatus status)
        {
            var bsonId = new BsonBinaryData(droneId);

            var filter = _filterDef.And(_filterDef.Eq(_ => _.DroneId, bsonId), _filterDef.Eq(_ => _.Status, status.ToString()));

            var p = (await _pedidolCollection.FindAsync(filter)).ToList();

            return p.ConvertTo<List<PedidoEntity>>();

        }

        public async Task<PedidoEntity> GetSingleByDroneID(Guid droneId)
        {
            var bsonId = new BsonBinaryData(droneId);

            var filter = _filterDef.And(_filterDef.Eq(_ => _.DroneId, bsonId), _filterDef.Eq(_ => _.Status, PedidoStatus.EmTransito.ToString()));

            var p = (await _pedidolCollection.FindAsync(filter)).FirstOrDefault();
            
            return p.ConvertTo<PedidoEntity>();

        }

        public async Task Inserir(PedidoEntity pedido)
        {
            var model = pedido.ConvertTo<Pedido>();


            await _pedidolCollection.InsertOneAsync(model);
           

        }

        public async Task Atualizar(PedidoEntity pedido)
        {
            var model = pedido.ConvertTo<Pedido>();
            var bsonId = new BsonBinaryData(pedido.Id);

            var filter = _filterDef.Eq(_ => _.Id, bsonId);
            var options = new FindOneAndReplaceOptions<Pedido>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

             await _pedidolCollection.FindOneAndReplaceAsync(filter, model, options);


        }

   
    }
}