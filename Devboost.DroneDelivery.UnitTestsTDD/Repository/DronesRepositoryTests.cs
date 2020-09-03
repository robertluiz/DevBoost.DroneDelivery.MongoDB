using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Api.Controllers;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Interfaces.Services;
using Devboost.DroneDelivery.Domain.Params;
using Devboost.DroneDelivery.UnitTestsTDD.Repository;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Devboost.DroneDelivery.Tests.Repository
{

    public class DronesRepositoryTests
    {
        private readonly IAutoFaker _autoFaker;
        private readonly IDronesRepository _repositoryDrone;     

        public DronesRepositoryTests()
        {
            _autoFaker = AutoFaker.Create();
            
        }

        [Fact(DisplayName = "DroneAtualizar")]
        [Trait("DronesRepositoryTests", "Repository Drone Tests")]
        public void Atualizar()
        {
            var drone = _autoFaker.Generate<DroneEntity>();
            var result  = _repositoryDrone.Atualizar(drone);                    
            Assert.Equal(Task.CompletedTask, result);
        }

        [Fact(DisplayName = "DroneGetAll")]
        [Trait("DronesRepositoryTests", "Repository Drone Tests")]
        public void GetAll()
        {
            var result = _repositoryDrone.GetAll();

            Assert.NotNull(result);            
        }

        [Fact(DisplayName = "DroneGetByStatus")]
        [Trait("DronesRepositoryTests", "Repository Drone Tests")]
        public void GetByStatus()
        {
            var status = _autoFaker.Generate<DroneStatus>();

            var result  =_repositoryDrone.GetByStatus(status.ToString());           

            Assert.Equal(status.ToString(), result.Result.FirstOrDefault().Status.ToString());
        }

        [Fact(DisplayName = "DroneIncluir")]
        [Trait("DronesRepositoryTests", "Repository Drone Tests")]
        public void Incluir()
        {
            var drone = _autoFaker.Generate<DroneEntity>();

            var result = _repositoryDrone.Incluir(drone);

            Assert.Equal(Task.CompletedTask, result);
        }        
    }
}