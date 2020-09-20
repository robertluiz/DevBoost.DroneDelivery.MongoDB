//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoBogus;
//using AutoMoqCore;
//using Devboost.DroneDelivery.Domain.Entities;
//using Devboost.DroneDelivery.Domain.Enums;
//using Devboost.DroneDelivery.Mongo;
//using Devboost.DroneDelivery.Repository.Implementation;
//using Devboost.DroneDelivery.Repository.Models;
//using KellermanSoftware.CompareNetObjects;
//using MongoDB.Driver;
//using Moq;
//using ServiceStack;
//using ServiceStack.OrmLite;
//using Xunit;

//namespace Devboost.DroneDelivery.UnitTestsTDD.Repository
//{
//    public class PedidoRepositoryTests
//    {
//        private readonly IAutoFaker _faker;
//        private readonly CompareLogic _comparison;
        
//        public PedidoRepositoryTests()
//        {
//            _faker = AutoFaker.Create();
//            _comparison = new CompareLogic();
//        }

//        [Fact(DisplayName = "Atualizar")]
//        [Trait("PedidosRepositoryTests", "Repository Tests")]
//        public async Task Atualizar_test()
//        {
//            //Given(Preparação)
//            var mocker = new AutoMoqer();
//            var collectionMock = new Mock<IMongoCollection<Pedido>>();
//            var service = new Mock<IMongoDatabase>();

            

//            var faker = AutoFaker.Create();


//            var expectresult = _faker.Generate<Pedido>();
//            expectresult.Peso = 1;

//            var param = expectresult.ConvertTo<PedidoEntity>();



//            collectionMock.Setup(c=> 
//                c.FindOneAndReplaceAsync(
//                     It.IsAny<FilterDefinition<Pedido>>(), 
//                It.IsAny<Pedido>(), 
//                    It.IsAny<FindOneAndReplaceOptions<Pedido>>(),
//            It.IsAny<CancellationToken>()))
//                .Returns(Task.Factory.StartNew(()=>expectresult))
//                .Verifiable();
//            service.Setup(r => r.GetCollection<Pedido>("Pedido", null)).Returns(collectionMock.Object).Verifiable();
//            var baseRepositoryMock = new PedidosRepository(service.Object);

//            //When
//            await baseRepositoryMock.Atualizar(param);

//            //Then
//            var comparison = new CompareLogic();
//            service.Verify(mock => mock.GetCollection<Pedido>("Pedido", null), Times.Once());
//            collectionMock.Verify(mock => mock.FindOneAndReplaceAsync(
//                It.IsAny<FilterDefinition<Pedido>>(),
//                It.IsAny<Pedido>(),
//                It.IsAny<FindOneAndReplaceOptions<Pedido>>(),
//                It.IsAny<CancellationToken>()), Times.Once());
            
            
       
//        }

//        [Fact(DisplayName = "Inserir")]
//        [Trait("PedidosRepositoryTests", "Repository Tests")]
//        public async Task Inserir_test()
//        {

//            //Given(Preparação)

//            var collectionMock = new Mock<IMongoCollection<Pedido>>();
//            var service = new Mock<IMongoDatabase>();


//            var expectresult = _faker.Generate<Pedido>();
//            var param = expectresult.ConvertTo<PedidoEntity>();



//            collectionMock.Setup(c =>
//                    c.InsertOneAsync(It.IsAny<Pedido>(), null,
//                        It.IsAny<CancellationToken>()))
//                .Returns(Task.Factory.StartNew(() => string.Empty))
//                .Verifiable();
//            service.Setup(r => r.GetCollection<Pedido>("Pedido", null)).Returns(collectionMock.Object).Verifiable();
//            var baseRepositoryMock = new PedidosRepository(service.Object);

//            //When
//            await baseRepositoryMock.Inserir(param);

//            //Then
         
//            service.Verify(mock => mock.GetCollection<Pedido>("Pedido", null), Times.Once());
//            collectionMock.Verify(mock => mock.InsertOneAsync(It.IsAny<Pedido>(), null,
//                It.IsAny<CancellationToken>()),Times.Once());

//        } 
        
//        [Fact(DisplayName = "GetAll")]
//        [Trait("PedidosRepositoryTests", "Repository Tests")]
//        public async Task GetAll_test()
//        {
          
            
       
//        }
        
//        [Fact(DisplayName = "GetByDroneID")]
//        [Trait("PedidosRepositoryTests", "Repository Tests")]
//        public async Task GetByDroneID_test()
//        {
//            //Given(Preparação)
         
            
       
//        }
        
//        [Fact(DisplayName = "GetSingleByDroneID")]
//        [Trait("PedidosRepositoryTests", "Repository Tests")]
//        public async Task GetSingleByDroneID_test()
//        {
       
       
//        }
        
//        [Fact(DisplayName = "GetByDroneIDAndStatus")]
//        [Trait("PedidosRepositoryTests", "Repository Tests")]
//        public async Task GetByDroneIDAndStatus_test()
//        {
//            //Given(Preparação)
  
            
       
//        }
//    }
//}