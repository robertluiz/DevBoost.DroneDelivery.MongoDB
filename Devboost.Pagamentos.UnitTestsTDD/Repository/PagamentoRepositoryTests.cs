using System;
using System.Threading.Tasks;
using AutoBogus;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.IoC;
using Devboost.Pagamentos.Repository.Implementation;
using Devboost.Pagamentos.Repository.Model;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.OrmLite;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.Repository
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

            var pagamento = await dbconnection.LoadSingleByIdAsync<Pagamento>(expectresult.Id);
           

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

            var expectresult = new AutoFaker<Pagamento>()
                .RuleFor(fake => fake.CartaoId, fake => Cartao.Id)
                .RuleFor(fake => fake.Cartao, fake => Cartao)
                .RuleFor(fake => fake.Boleto, fake => null)
                .RuleFor(fake => fake.BoletoId, fake => null)
                .RuleFor(fake => fake.Valor, fake => 1)
                .Generate();
            return expectresult;
        }



        [Fact(DisplayName = "Update")]
        [Trait("PagamentoRepositoryTests", "Repository Tests")]
        public async Task Update_test()
        {
            //Given(Prepara��o)
            using var dbconnection = await new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider).OpenAsync();
            var baseRepositoryMock = new PagamentoRepository(dbconnection);

            dbconnection.CreateTableIfNotExists<Cartao>();
            dbconnection.CreateTableIfNotExists<Boleto>();
            dbconnection.CreateTableIfNotExists<Pagamento>();


            var paramInit = Expectresult();
            await dbconnection.SaveAsync(paramInit, references: true);
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
