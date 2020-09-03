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
    public class DronesRepositoryFake: IDronesRepository
    {
        private readonly List<DroneEntity> _drones;
        private readonly IAutoFaker _autoFaker;

        public DronesRepositoryFake()
        {
            _autoFaker = AutoFaker.Create();
            _drones = _autoFaker.Generate<List<DroneEntity>>();
        }

        public Task Atualizar(DroneEntity drone)
        {
            _drones.FirstOrDefault().DataAtualizacao = DateTime.Now;
            return Task.CompletedTask;
        }

        public Task<List<DroneEntity>> GetAll()
        {
            return Task.FromResult(_drones);
        }

        public Task<List<DroneEntity>> GetByStatus(string status)
        {
            return Task.FromResult(_drones.Where(s => s.Status.ToString() == status).ToList());
        }

        public Task Incluir(DroneEntity drone)
        {
            _drones.Add(drone);
            return Task.CompletedTask;
        }
    }
}