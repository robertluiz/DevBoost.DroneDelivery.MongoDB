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
                to.Cartao = from.ConvertTo<CartaoEntity>(skipConverters: true);
                return to;
            });

            AutoMapping.RegisterConverter((CartaoEntity from) => {
                var to = from.ConvertTo<Cartao>(skipConverters: true);                
                return to;
            });

            AutoMapping.RegisterConverter((PagamentoEntity from) => {
                var to = from.ConvertTo<Pagamento>(skipConverters: true);
                return to;
            });

            return services;
        }
    }
}