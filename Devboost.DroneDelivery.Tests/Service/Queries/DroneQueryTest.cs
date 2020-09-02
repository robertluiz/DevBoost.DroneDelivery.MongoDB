using System;
using AutoMoqCore;
using System.Collections.Generic;
using System.Linq;
using AutoBogus;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.DomainService.Queries;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Xunit;

namespace Devboost.DroneDelivery.Tests.Service.Queries
{
    public class DroneQueryTest
    {
        [Fact(DisplayName = "ConsultaDrone")]
        [Trait("DroneQuery", "Query Tests")]
        public async void ConsultaDrone_Test()
        {
            var mocker = new AutoMoqer();
            var baseQueryMock = mocker.Create<DroneQuery>();

            var faker = AutoFaker.Create();

            var responseDroneRepo = faker.Generate<List<DroneEntity>>();
            var responseUsuarioRepo = faker.Generate<UsuarioEntity>();
            var responsePedidosRepo = new List<PedidoEntity>
            {
                new AutoFaker<PedidoEntity>()
                    .RuleFor(fake => fake.Drone, fake => responseDroneRepo[0])
                    .RuleFor(fake => fake.DroneId, fake => responseDroneRepo[0].Id)
                    .RuleFor(fake => fake.CompradorId, fake => responseUsuarioRepo.Id)
                    .Generate()
            };
            

            var expectResponse = ExpectResponseDroneQueryList(responseDroneRepo, responsePedidosRepo, responseUsuarioRepo); ;

            var droneRepoMock = mocker.GetMock<IDronesRepository>();
            var pedidosRepoMock = mocker.GetMock<IPedidosRepository>();
            var usuarioRepoMock = mocker.GetMock<IUsuariosRepository>();
            droneRepoMock.Setup(r => r.GetAll()).ReturnsAsync(responseDroneRepo).Verifiable();
            pedidosRepoMock.Setup(r => r.GetByDroneID(responseDroneRepo[0].Id)).ReturnsAsync(responsePedidosRepo).Verifiable();
            usuarioRepoMock.Setup(r => r.GetSingleById(responseUsuarioRepo.Id)).ReturnsAsync(responseUsuarioRepo).Verifiable();

            //When

            var result = await baseQueryMock.ConsultaDrone();

            //Then
            var comparison = new CompareLogic();
            droneRepoMock.Verify(mock => mock.GetAll(), Times.Once());
            pedidosRepoMock.Verify(mock => mock.GetByDroneID(It.IsAny<Guid>()), Times.Exactly(3));
            usuarioRepoMock.Verify(mock => mock.GetSingleById(It.IsAny<Guid>()), Times.Once());
            var comp = comparison.Compare(result, expectResponse);

            Assert.True(comp.AreEqual);
        }

        private static List<ConsultaDronePedidoDTO> ExpectResponseDroneQueryList(List<DroneEntity> responseDroneRepo, List<PedidoEntity> responsePedidosRepo, UsuarioEntity responseUsuarioRepo)
        {
            return responseDroneRepo.Select(async d =>
                {
                    var p = responsePedidosRepo[0];
                    List<PedidoEntity> _pedidos = null;
                    List<ConsultaPedidoCompradorDTO> comprador = null;
                    if (p.DroneId.Equals(d.Id))
                    {
                        _pedidos = responsePedidosRepo;
                        comprador = new List<ConsultaPedidoCompradorDTO> {
                            new ConsultaPedidoCompradorDTO
                            {
                                PedidoId = p.Id,
                                Status = p.Status,
                                CompradorId = p.CompradorId,
                                NomeComprador = responseUsuarioRepo.Nome,
                                DataHora = p.DataHora,
                                DistanciaDaEntrega = p.DistanciaDaEntrega,
                                Peso = p.Peso,
                                Latitude = responseUsuarioRepo.Latitude,
                                Longitude = responseUsuarioRepo.Longitude
                            }
                        };

                    }


                    return new ConsultaDronePedidoDTO
                    {

                        IdDrone = d.Id,
                        Situacao = d.Status.ToString(),
                        Pedidos = _pedidos,
                        PedidosComprador = comprador
                    };
                    
                })
                .ToList()
                .Select(c => c.Result)
                .ToList();
        }
    }
}