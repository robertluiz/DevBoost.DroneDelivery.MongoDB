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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Devboost.DroneDelivery.Tests.Repository
{

    public class PedidoRepositoryTests
    {
        private readonly IAutoFaker _autoFaker;
        private readonly IPedidosRepository _repositoryPedido;

        public PedidoRepositoryTests()
        {
            _autoFaker = AutoFaker.Create();
            _repositoryPedido = new PedidosRepositoryFake();
        }

        [Fact(DisplayName = "PedidoAtualizar")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void Atualizar()
        {
            var pedido = _autoFaker.Generate<PedidoEntity>();
            var result = _repositoryPedido.Atualizar(pedido);
            Assert.Equal(Task.CompletedTask, result);
        }

        [Fact(DisplayName = "PedidoGetAll")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void GetAll()
        {
            var result = _repositoryPedido.GetAll();

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "PedidoGetByDroneID")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void GetByDroneID()
        {
            var id = _autoFaker.Generate<Guid>();

            var result = _repositoryPedido.GetByDroneID(id);

            Assert.Equal(id, result.Result.FirstOrDefault().DroneId);
        }

        [Fact(DisplayName = "PedidoGetByDroneIDAndStatus")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void GetByDroneIDAndStatus()
        {

            var id = _autoFaker.Generate<Guid>();
            var status = _autoFaker.Generate<PedidoStatus>();

            var result = _repositoryPedido.GetByDroneIDAndStatus(id, status);

            Assert.Equal(id, result.Result.FirstOrDefault().DroneId);
            Assert.Equal(status.ToString(), result.Result.FirstOrDefault().Status);
        }

        [Fact(DisplayName = "PedidoGetSingleByDroneID")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void GetSingleByDroneID()
        {
            var id = _autoFaker.Generate<Guid>();

            var result = _repositoryPedido.GetSingleByDroneID(id);

            Assert.Equal(id, result.Result.DroneId);
        }

        [Fact(DisplayName = "PedidoInserir")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void Inserir()
        {
            var pedido = _autoFaker.Generate<PedidoEntity>();

            var result = _repositoryPedido.Inserir(pedido);

            Assert.Equal(Task.CompletedTask, result);
        }
    }
}