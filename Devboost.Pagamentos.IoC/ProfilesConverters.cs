using System.Diagnostics.CodeAnalysis;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Params;
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
                to.FormaPagamento = new FormaPagamentoEntity
                {
                    Cartao = from.ConvertTo<CartaoEntity>(skipConverters: true)
                };
                return to;
            });

            return services;
        }
    }
}