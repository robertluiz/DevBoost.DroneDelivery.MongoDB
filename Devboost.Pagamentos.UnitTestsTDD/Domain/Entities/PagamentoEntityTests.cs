using AutoBogus;
using Bogus;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.Entity;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Devboost.Pagamentos.UnitTestsTDD.Domain.Entities
{
    public class PagamentoEntityTests
    {
        [Fact(DisplayName = "Validar dados pagamento")]
        [Trait("PagamentoEntityTests", "Entities Tests")]
        public void ValidarDadosPagamento_Test_Sucesso()
        {
            //Given

            var cartaoEntity = new AutoFaker<CartaoEntity>()
                                    .RuleFor(Faker => Faker.Bandeira, fake => PagamentoBandeiraEnum.MasterCard)
                                    .RuleFor(Faker => Faker.CodSeguranca, fake => (new Random().Next(100, 999)).ToString())
                                    .RuleFor(Faker => Faker.NumeroCartao, fake => "1245121554899654")
                                    .RuleFor(Faker => Faker.DataValidade, fake => DateTime.Now.AddYears(2))
                                    .RuleFor(Faker => Faker.Tipo, fake => TipoCartaoEnum.Credito);

            var formaPagamentoEntity = new AutoFaker<FormaPagamentoEntity>()
                                    .RuleFor(Faker => Faker.Cartao, fake => cartaoEntity);                                    

            var param = new AutoFaker<PagamentoEntity>()
                .RuleFor(fake => fake.StatusPagamento, fake => StatusPagamentoEnum.Pendente)
                .RuleFor(fake => fake.Valor, fake => new Random().Next(10, 100000))                
                .RuleFor(fake => fake.FormaPagamento, formaPagamentoEntity)
                .Generate();

            var result = param.Validar();           

            Assert.True(result.Count.Equals(0));
        }

        [Fact(DisplayName = "Validar dados pagamento Erro")]
        [Trait("PagamentoEntityTests", "Entities Tests")]
        public void ValidarDadosPagamento_Test_Erro()
        {
            //Given
 

            var cartaoEntity = new AutoFaker<CartaoEntity>()
                                    .RuleFor(Faker => Faker.Bandeira, fake => PagamentoBandeiraEnum.MasterCard)
                                    .RuleFor(Faker => Faker.CodSeguranca, fake => (new Random().Next(10, 999)).ToString())
                                    .RuleFor(Faker => Faker.NumeroCartao, fake => "124512155")
                                    .RuleFor(Faker => Faker.DataValidade, fake => DateTime.Now.AddYears(-2))
                                    .RuleFor(Faker => Faker.Tipo, fake => TipoCartaoEnum.Credito);

            var formaPagamentoEntity = new AutoFaker<FormaPagamentoEntity>()
                                    .RuleFor(Faker => Faker.Cartao, fake => cartaoEntity);

            var param = new AutoFaker<PagamentoEntity>()
                .RuleFor(fake => fake.StatusPagamento, fake => StatusPagamentoEnum.Pendente)
                .RuleFor(fake => fake.Valor, fake => new Random().Next(10, 100000))
                .RuleFor(fake => fake.FormaPagamento, formaPagamentoEntity)
                .Generate();

            var result = param.Validar();

            Assert.True(result.Count == 2);
        }
    }
}