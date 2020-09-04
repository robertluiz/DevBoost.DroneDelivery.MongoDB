using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Repository.Implementation;
using Devboost.DroneDelivery.Repository.Models;
using KellermanSoftware.CompareNetObjects;
using ServiceStack;
using ServiceStack.OrmLite;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsTDD.Repository
{
    public class UsuariosRepositoryTests
    {
        private readonly IAutoFaker _faker;
        private readonly CompareLogic _comparison;
        
        public UsuariosRepositoryTests()
        {
            _faker = AutoFaker.Create();
            _comparison = new CompareLogic();
        }
        
        [Fact(DisplayName = "Inserir")]
        [Trait("UsuariosRepositoryTests", "Repository Tests")]
        public async Task Inserir_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new UsuariosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Usuario>();
            var expectresult = _faker.Generate<Usuario>();
            
            var param = expectresult.ConvertTo<UsuarioEntity>();
            
            //When
            
            await baseRepositoryMock.Inserir(param);
            var result = await dbconnection.SingleAsync<Usuario>(p=> p.Id == expectresult.Id);
            
            //Then
            
            Assert.True(_comparison.Compare(result, expectresult).AreEqual);
            
       
        }
        
        [Fact(DisplayName = "GetAll")]
        [Trait("UsuariosRepositoryTests", "Repository Tests")]
        public async Task GetAll_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new UsuariosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Usuario>();
            var expectresult = _faker.Generate<Usuario>(4);
            
            await dbconnection.InsertAllAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetAll();
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<List<Usuario>>(), expectresult).AreEqual);
            
       
        }
        
        [Fact(DisplayName = "GetSingleById")]
        [Trait("UsuariosRepositoryTests", "Repository Tests")]
        public async Task GetSingleById_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new UsuariosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Usuario>();
            var param = Guid.NewGuid();
            var expectresult = new AutoFaker<Usuario>()
                .RuleFor(fake => fake.Id, fake =>param)
                .Generate();
            await dbconnection.InsertAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetSingleById(param);
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<Usuario>(), expectresult).AreEqual);
            
       
        }
        
        [Fact(DisplayName = "GetSingleByLogin")]
        [Trait("UsuariosRepositoryTests", "Repository Tests")]
        public async Task GetSingleByLogin_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new UsuariosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Usuario>();
            var param = "fulano";
            var expectresult = new AutoFaker<Usuario>()
                .RuleFor(fake => fake.Login, fake =>param)
                .Generate();
            await dbconnection.InsertAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetSingleByLogin(param);
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<Usuario>(), expectresult).AreEqual);
            
       
        }
        
        [Fact(DisplayName = "GetUsuarioByLoginSenha")]
        [Trait("UsuariosRepositoryTests", "Repository Tests")]
        public async Task GetUsuarioByLoginSenha_test()
        {
            //Given(Preparação)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new UsuariosRepository(dbconnection);
            
            dbconnection.CreateTableIfNotExists<Usuario>();
            
            var expectresult = new AutoFaker<Usuario>()
                .Generate();
            var param = new UsuarioEntity
            {
                Login = expectresult.Login,
                Senha = expectresult.Senha
            };
            await dbconnection.InsertAsync(expectresult);
            
            //When
            
            var result = await baseRepositoryMock.GetUsuarioByLoginSenha(param);
            
            
            //Then
            
            Assert.True(_comparison.Compare(result.ConvertTo<Usuario>(), expectresult).AreEqual);
            
       
        }
    }
}