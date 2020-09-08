using Devboost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Domain.Interfaces.Repository
{
    public interface IDronesRepository
    {
        Task<List<DroneEntity>> GetAll();
        Task<DroneEntity> GetByID(Guid droneId);
        Task<List<DroneEntity>> GetByStatus(string status);
        Task Atualizar(DroneEntity drone);
        Task Incluir(DroneEntity drone);

    }
}
