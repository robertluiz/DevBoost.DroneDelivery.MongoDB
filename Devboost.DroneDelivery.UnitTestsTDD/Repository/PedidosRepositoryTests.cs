using AutoBogus;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.UnitTestsTDD.Repository;
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
        private readonly List<PedidoEntity> _pedidos;

        public PedidoRepositoryTests()
        {
            _pedidos = new AutoFaker<PedidoEntity>().RuleFor(fake => fake.Status, fake => PedidoStatus.EmTransito.ToString()).Generate(5);
            _repositoryPedido = new PedidosRepositoryFake(_pedidos);
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
            var id = _pedidos.FirstOrDefault().DroneId;

            var result = _repositoryPedido.GetByDroneID(id);

            Assert.Equal(id, result.Result.FirstOrDefault().DroneId);
        }

        [Fact(DisplayName = "PedidoGetByDroneIDAndStatus")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void GetByDroneIDAndStatus()
        {
            var pedido = _pedidos.FirstOrDefault();
            var id = pedido.DroneId;
            var status = (PedidoStatus)Enum.Parse(typeof(PedidoStatus), pedido.Status, true);

            var result = _repositoryPedido.GetByDroneIDAndStatus(id, status);

            Assert.Equal(id, result.Result.FirstOrDefault().DroneId);
            Assert.Equal(status.ToString(), result.Result.FirstOrDefault().Status);
        }

        [Fact(DisplayName = "PedidoGetSingleByDroneID")]
        [Trait("PedidoRepositoryTests", "Repository Pedido Tests")]
        public void GetSingleByDroneID()
        {
            var id = _pedidos.FirstOrDefault().DroneId;

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