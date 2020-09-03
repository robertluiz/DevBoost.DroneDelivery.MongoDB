using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.DomainService.Commands;
using Moq;
using Xunit;

namespace Devboost.DroneDelivery.Tests.Service.Commands
{
    public class DroneCommandTest
    {
        [Fact(DisplayName = "LiberaDrone")]
        [Trait("DroneCommand", "Command Tests")]
        public async void ConsultaDrone_Test()
        {
            var mocker = new AutoMoqer();
            var baseCommandMock = mocker.Create<DroneCommand>();


            var responseDroneRepo = new AutoFaker<DroneEntity>()
                .RuleFor(fake => fake.Status, fake => DroneStatus.Pronto)
                .RuleFor(fake => fake.Autonomia, fake => 35)
                .RuleFor(fake => fake.Capacidade, fake => 12000)
                .RuleFor(fake => fake.Velocidade, fake => 60)
                .RuleFor(fake => fake.DataAtualizacao, fake => fake.Date.Recent())
                .Generate(3);

            var pedidosDrone0 = new AutoFaker<PedidoEntity>()
                .RuleFor(fake => fake.DroneId, fake => responseDroneRepo[0].Id)
                .RuleFor(fake => fake.Drone, fake => responseDroneRepo[0])
                .Generate(1);
            var pedidosDrone1 = new AutoFaker<PedidoEntity>()
                .RuleFor(fake => fake.DroneId, fake => responseDroneRepo[1].Id)
                .RuleFor(fake => fake.Drone, fake => responseDroneRepo[1])
                .Generate(1);
           
            var responseQueryDrone0 = new AutoFaker<ConsultaDronePedidoDTO>()
                .RuleFor(fake => fake.IdDrone, fake => responseDroneRepo[0].Id)
                .RuleFor(fake => fake.Pedidos, fake => pedidosDrone0)
                .Generate();
            var responseQueryDrone1 = new AutoFaker<ConsultaDronePedidoDTO>()
                .RuleFor(fake => fake.IdDrone, fake => responseDroneRepo[1].Id)
                .RuleFor(fake => fake.Pedidos, fake => pedidosDrone1)
                .Generate();
        var responseQueryDrone2 = new AutoFaker<ConsultaDronePedidoDTO>()
                .RuleFor(fake => fake.IdDrone, fake => responseDroneRepo[2].Id)
                .RuleFor(fake => fake.Pedidos, fake => new List<PedidoEntity>())
                .Generate();
        


            

            var droneRepoMock = mocker.GetMock<IDronesRepository>();
            var droneQueryMock = mocker.GetMock<IDroneQuery>();
            droneRepoMock.Setup(r => r.GetByStatus(It.IsAny<string>())).ReturnsAsync(responseDroneRepo).Verifiable();
            droneRepoMock.Setup(r => r.Atualizar(It.IsAny<DroneEntity>())).Returns(Task.Factory.StartNew(()=> string.Empty)).Verifiable();
            droneQueryMock.Setup(r => r.RetornaConsultaDronePedido(responseDroneRepo[0])).ReturnsAsync(responseQueryDrone0).Verifiable();
            droneQueryMock.Setup(r => r.RetornaConsultaDronePedido(responseDroneRepo[1])).ReturnsAsync(responseQueryDrone1).Verifiable();
            droneQueryMock.Setup(r => r.RetornaConsultaDronePedido(responseDroneRepo[2])).ReturnsAsync(responseQueryDrone2).Verifiable();
            

            //When

            await baseCommandMock.LiberaDrone();

            //Then

            droneRepoMock.Verify(mock => mock.GetByStatus(It.IsAny<string>()), Times.Once());
            droneRepoMock.Verify(mock => mock.Atualizar(It.IsAny<DroneEntity>()), Times.Exactly(2));
            droneQueryMock.Verify(mock => mock.RetornaConsultaDronePedido(responseDroneRepo[0]), Times.Once());
            droneQueryMock.Verify(mock => mock.RetornaConsultaDronePedido(responseDroneRepo[1]), Times.Once());
            droneQueryMock.Verify(mock => mock.RetornaConsultaDronePedido(responseDroneRepo[2]), Times.Once());
            
        }
    }
}