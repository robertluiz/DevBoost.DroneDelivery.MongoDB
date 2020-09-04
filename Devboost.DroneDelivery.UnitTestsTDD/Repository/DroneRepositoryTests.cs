using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Repository.Implementation;
using Devboost.DroneDelivery.Repository.Models;
using KellermanSoftware.CompareNetObjects;
using Moq;
using ServiceStack;
using ServiceStack.OrmLite;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsTDD.Repository
{
	public class DroneRepositoryTests
	{
		private readonly IAutoFaker _faker;
		private readonly CompareLogic _comparison;

		public DroneRepositoryTests()
		{
			_faker = AutoFaker.Create();
			_comparison = new CompareLogic();
		}

		[Fact(DisplayName = "GetAll")]
		[Trait("DroneRepositoryTests", "Repository Tests")]
		public async Task GetAll_test()
		{
			//Given(Preparação)
			using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
			
			dbconnection.CreateTableIfNotExists<Drone>();
			var expectresult = new AutoFaker<Drone>()
				.RuleFor(fake => fake.Status, fake => DroneStatus.Pronto.ToString())
				.Generate(4);

			await dbconnection.InsertAllAsync(expectresult);
			var baseRepositoryMock = new DronesRepository(dbconnection);

			//When

			var result = await baseRepositoryMock.GetAll();

			//Then
			var comparacao = _comparison.Compare(result.ConvertTo<List<Drone>>(), expectresult);
			Assert.True(comparacao.AreEqual);
		}

		[Fact(DisplayName = "GetByStatus")]
		[Trait("DroneRepositoryTests", "Repository Tests")]
		public async Task GetByStatus_test()
		{
			//Given(Preparação)
			using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
			var baseRepositoryMock = new DronesRepository(dbconnection);

			dbconnection.CreateTableIfNotExists<Drone>();
			var expectresult = new AutoFaker<Drone>()
				.RuleFor(fake => fake.Status, fake => DroneStatus.Pronto.ToString())
				.Generate(4);

			await dbconnection.InsertAllAsync(expectresult);

			//When

			var result = await baseRepositoryMock.GetByStatus(DroneStatus.Pronto.ToString());

			//Then
			var comparacao = _comparison.Compare(result.ConvertTo<List<Drone>>(), expectresult);
			Assert.True(comparacao.AreEqual);
		}

		[Fact(DisplayName = "Incluir")]
		[Trait("DroneRepositoryTests", "Repository Tests")]
		public async Task Incluir_test()
		{
			//Given(Preparação)
			using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
			var baseRepositoryMock = new DronesRepository(dbconnection);

			dbconnection.CreateTableIfNotExists<Drone>();
			var expectresult = new AutoFaker<Drone>()
				.RuleFor(fake => fake.Status, fake => DroneStatus.Pronto.ToString())
				.Generate(1);

			var param = expectresult[0].ConvertTo<DroneEntity>();

			//When

			await baseRepositoryMock.Incluir(param);
			var result = await dbconnection.SingleAsync<Drone>(p => p.Id == expectresult[0].Id);

			//Then

			Assert.True(_comparison.Compare(result, expectresult[0]).AreEqual);
		}

		[Fact(DisplayName = "Atualizar")]
		[Trait("DroneRepositoryTests", "Repository Tests")]
		public async Task Atualizar_test()
		{
			//Given(Preparação)
			using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
			var baseRepositoryMock = new DronesRepository(dbconnection);

			dbconnection.CreateTableIfNotExists<Drone>();
			var expectresult = _faker.Generate<Drone>();

			await dbconnection.InsertAsync(expectresult);
			expectresult.Status = DroneStatus.EmTransito.ToString();

			var param = expectresult.ConvertTo<DroneEntity>();

			//When

			await baseRepositoryMock.Atualizar(param);
			var result = await dbconnection.SingleAsync<Drone>(p => p.Id == expectresult.Id);

			//Then

			Assert.True(_comparison.Compare(result, expectresult).AreEqual);
		}

	}
}