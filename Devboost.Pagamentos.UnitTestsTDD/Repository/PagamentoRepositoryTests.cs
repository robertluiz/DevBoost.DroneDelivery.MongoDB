using AutoBogus;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Repository.Implementation;
using Devboost.Pagamentos.Repository.Model;
using KellermanSoftware.CompareNetObjects;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Devboost.Pagamentos.IoC;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Devboost.Pagamentos.Domain.Enums;

namespace Devboost.Pagamentos.UnitTestsTDD
{
	public class PagamentoRepositoryTests
	{
		private readonly IAutoFaker _faker;
		private readonly CompareLogic _comparison;
	

		public PagamentoRepositoryTests()
		{
			_faker = AutoFaker.Create();
			_comparison = new CompareLogic();
            var serviceCollection = new ServiceCollection();
			serviceCollection.ResolveConverters().BuildServiceProvider();
        }

		[Fact(DisplayName = "AddUsingRef")]
		[Trait("PagamentoRepositoryTests", "Repository Tests")]
		public async Task AddUsingRef_Test()
		{
			using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
		
			var baseRepositoryMock = new PagamentoRepository(dbconnection);

			var expectresult = Expectresult();

            var param = expectresult.ConvertTo<PagamentoEntity>();
			//When
			await baseRepositoryMock.AddUsingRef(param);

            //Then

			var pagamento = await dbconnection.SingleByIdAsync<Pagamento>(expectresult.Id);
            pagamento.FormaPagamento = await dbconnection.LoadSingleByIdAsync<FormaPagamento>(pagamento.FormaPagamentoId);


			var comparacao = _comparison.Compare(pagamento, expectresult);

			Assert.True(comparacao.AreEqual);
			
		}

        private static Pagamento Expectresult()
        {
            var guidCartao = Guid.NewGuid();
            var datetime = DateTime.Now;
            var Cartao = new AutoFaker<Cartao>()
                .RuleFor(fake => fake.Id, fake => guidCartao)
                .RuleFor(fake => fake.Bandeira, fake => PagamentoBandeiraEnum.MasterCard)
                .RuleFor(fake => fake.DataValidade, fake => datetime)
                .RuleFor(fake => fake.Tipo, fake => TipoCartaoEnum.Credito)
                .Generate();

            var formaPagamento = new AutoFaker<FormaPagamento>()
                .RuleFor(fake => fake.CartaoId, fake => guidCartao)
                .RuleFor(fake => fake.Cartao, fake => Cartao)
                .Generate();

            var expectresult = new AutoFaker<Pagamento>()
                .RuleFor(fake => fake.FormaPagamentoId, fake => formaPagamento.Id)
                .RuleFor(fake => fake.FormaPagamento, fake => formaPagamento)
                .RuleFor(fake => fake.Valor, fake => 1)
                .Generate();
            return expectresult;
        }


        [Fact(DisplayName = "Update")]
		[Trait("PagamentoRepositoryTests", "Repository Tests")]
		public async Task Update_test()
		{
			//Given(Preparação)
			using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
			var baseRepositoryMock = new PagamentoRepository(dbconnection);

            dbconnection.CreateTableIfNotExists<Cartao>();
            dbconnection.CreateTableIfNotExists<FormaPagamento>();
            dbconnection.CreateTableIfNotExists<Pagamento>();


            var paramInit = Expectresult();
			await dbconnection.SaveAsync(paramInit, references:true);
            var expectresult = (await dbconnection.SelectAsync<Pagamento>()).FirstNonDefault();


			expectresult.Valor = 2; 

			var param = expectresult.ConvertTo<PagamentoEntity>();

			//When
			await baseRepositoryMock.Update(param);
			var result = await dbconnection.SingleByIdAsync<Pagamento>(expectresult.Id);

			//Then

			Assert.True(result.Valor == expectresult.Valor);
		}

	}
}
