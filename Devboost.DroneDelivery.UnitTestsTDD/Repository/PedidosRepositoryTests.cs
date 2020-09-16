using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Mongo;
using Devboost.DroneDelivery.Repository.Implementation;
using Devboost.DroneDelivery.Repository.Models;
using KellermanSoftware.CompareNetObjects;
using Moq;
using ServiceStack;
using ServiceStack.OrmLite;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsTDD.Repository
{
    public class PedidoRepositoryTests
    {
        private readonly IAutoFaker _faker;
        private readonly CompareLogic _comparison;
        
        public PedidoRepositoryTests()
        {
            _faker = AutoFaker.Create();
            _comparison = new CompareLogic();
        }

        [Fact(DisplayName = "Atualizar")]
        [Trait("PedidosRepositoryTests", "Repository Tests")]
        public async Task Atualizar_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new PedidosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Pedido>();
            var expectresult = _faker.Generate<Pedido>();
            
            await dbconnection.InsertAsync(expectresult);
            expectresult.Peso = 1;
            
            var param = expectresult.ConvertTo<PedidoEntity>();
            
            //When
            
            await baseRepositoryMock.Atualizar(param);
            var result = await dbconnection.SingleAsync<Pedido>(p=> p.Id == expectresult.Id);
            
            //Then
            
            Assert.True(_comparison.Compare(result, expectresult).AreEqual);
            
       
        }

        [Fact(DisplayName = "Inserir")]
        [Trait("PedidosRepositoryTests", "Repository Tests")]
        public async Task Inserir_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new PedidosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Pedido>();
            var expectresult = _faker.Generate<Pedido>();
            
            var param = expectresult.ConvertTo<PedidoEntity>();
            
            //When
            
            await baseRepositoryMock.Inserir(param);
            var result = await dbconnection.SingleAsync<Pedido>(p=> p.Id == expectresult.Id);
            
            //Then
            
            Assert.True(_comparison.Compare(result, expectresult).AreEqual);
            
       
        } 
        
        [Fact(DisplayName = "GetAll")]
        [Trait("PedidosRepositoryTests", "Repository Tests")]
        public async Task GetAll_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new PedidosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Pedido>();
            var expectresult = _faker.Generate<Pedido>(4);
            
            await dbconnection.InsertAllAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetAll();
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<List<Pedido>>(), expectresult).AreEqual);
            
       
        }
        
        [Fact(DisplayName = "GetByDroneID")]
        [Trait("PedidosRepositoryTests", "Repository Tests")]
        public async Task GetByDroneID_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new PedidosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Pedido>();
            var param = Guid.NewGuid();
            var expectresult = new AutoFaker<Pedido>()
                .RuleFor(fake => fake.DroneId, fake =>param)
                .Generate(3);
            await dbconnection.InsertAllAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetByDroneID(param);
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<List<Pedido>>(), expectresult).AreEqual);
            
       
        }
        
        [Fact(DisplayName = "GetSingleByDroneID")]
        [Trait("PedidosRepositoryTests", "Repository Tests")]
        public async Task GetSingleByDroneID_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new PedidosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Pedido>();
            var param = Guid.NewGuid();
            var expectresult = new AutoFaker<Pedido>()
                .RuleFor(fake => fake.DroneId, fake =>param)
                .RuleFor(fake => fake.Status, fake => PedidoStatus.EmTransito.ToString())
                .Generate();
            await dbconnection.InsertAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetSingleByDroneID(param);
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<Pedido>(), expectresult).AreEqual);
            
       
        }
        
        [Fact(DisplayName = "GetByDroneIDAndStatus")]
        [Trait("PedidosRepositoryTests", "Repository Tests")]
        public async Task GetByDroneIDAndStatus_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new PedidosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Pedido>();
            
            var param1 = Guid.NewGuid();
            var param2 = PedidoStatus.Entregue;
            
            var expectresult = new AutoFaker<Pedido>()
                .RuleFor(fake => fake.DroneId, fake =>param1)
                .RuleFor(fake => fake.Status, fake => PedidoStatus.Entregue.ToString())
                .Generate(3);
            
            await dbconnection.InsertAllAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetByDroneIDAndStatus(param1,param2);
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<List<Pedido>>(), expectresult).AreEqual);
            
       
        }
    }
}