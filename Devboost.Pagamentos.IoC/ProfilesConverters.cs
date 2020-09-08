using System;
using System.Diagnostics.CodeAnalysis;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Params;
using Devboost.Pagamentos.Repository.Model;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;

namespace Devboost.Pagamentos.IoC
{
    public static class ProfilesConverters
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection ResolveConverters(this IServiceCollection services)
        {
            AutoMapping.RegisterConverter((CartaoParam from) => {
                var to = from.ConvertTo<PagamentoEntity>(skipConverters: true);
                to.Id = Guid.NewGuid();
                to.FormaPagamento = new FormaPagamentoEntity
                {
                    Cartao = from.ConvertTo<CartaoEntity>(skipConverters: true)
                };
                return to;
            });


            AutoMapping.RegisterConverter((CartaoEntity from) => {
                var to = from.ConvertTo<Cartao>(skipConverters: true);
                to.Id = from.Id ?? Guid.NewGuid();
                return to;
            });

            AutoMapping.RegisterConverter((FormaPagamentoEntity from) => {
                var to = from.ConvertTo<FormaPagamento>(skipConverters: true);
                if (to == null) return null;
                to.Id = from.Id ?? Guid.NewGuid();
                to.CartaoId = to.Cartao.Id;
                return to;
            });

            AutoMapping.RegisterConverter((PagamentoEntity from) => {
                var to = from.ConvertTo<Pagamento>(skipConverters: true);
                to.Id = from.Id ?? Guid.NewGuid();
                if (to.FormaPagamento != null) to.FormaPagamentoId = to.FormaPagamento.Id;
                return to;
            });

            return services;
        }
    }
}