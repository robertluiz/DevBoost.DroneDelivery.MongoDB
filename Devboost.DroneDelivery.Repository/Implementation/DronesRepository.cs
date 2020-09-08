using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Repository.Models;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class DronesRepository : IDronesRepository, IDisposable
    {
        private readonly IDbConnection _connection; 
        
        public DronesRepository(IDbConnection connection)
        {
            _connection = connection;
            
        }        

        public async Task<List<DroneEntity>> GetAll()
        {
            
            if (_connection.CreateTableIfNotExists<Drone>())
            {
                await _connection.InsertAllAsync(SeedDrone());
            }
            var list = await _connection.SelectAsync<Drone>();
                
            return list.ConvertTo<List<DroneEntity>>();
        }

        public async Task<DroneEntity> GetByID(Guid droneId)
        {

            _connection.CreateTableIfNotExists<Drone>();
            var d = await _connection.SingleByIdAsync<Drone>(droneId);

            return d.ConvertTo<DroneEntity>();

        }

        public async Task<List<DroneEntity>> GetByStatus(string status)
        {
           
            _connection.CreateTableIfNotExists<Drone>();
            
            var list = await _connection.SelectAsync<Drone>(d => d.Status == status);
            return list.ConvertTo<List<DroneEntity>>();
        }

        public async Task Atualizar(DroneEntity drone)
        {
            var model = drone.ConvertTo<Drone>();
           
            
            _connection.CreateTableIfNotExists<Drone>();
            await _connection.UpdateAsync(model);
            
        }

        public async Task Incluir(DroneEntity drone)
        {
            var model = drone.ConvertTo<Drone>();
           
            
            _connection.CreateTableIfNotExists<Drone>();
            await _connection.InsertAsync(model);
        }
        

        private static List<Drone> SeedDrone()
        {

            return new List<Drone>{
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35, //Minutos
                    Capacidade = 12000, //gramas
                    Velocidade = 60, //Km por h
                    Carga = 60, //Minutos para carregar totalmente
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
                new Drone
                {
                    Id = Guid.NewGuid(),
                    Status = DroneStatus.Pronto.ToString(),
                    Autonomia = 35,
                    Capacidade = 12000,
                    Velocidade = 60,
                    Carga = 60,
                    DataAtualizacao = DateTime.Now
                },
            };

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

        ~DronesRepository()
        {
            Dispose(false);
        }
    }
}