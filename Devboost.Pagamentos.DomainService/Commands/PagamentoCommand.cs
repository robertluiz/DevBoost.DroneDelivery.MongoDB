using System;
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Params;
using ServiceStack;

//using System.ComponentModel.DataAnnotations;

namespace Devboost.Pagamentos.DomainService.Commands
{
    public class PagamentoCommand: IPagamentoCommand
    {
        public PagamentoCommand()
        {
            AutoMapping.RegisterConverter((CartaoParam from) => {
                var to = from.ConvertTo<PagamentoEntity>(skipConverters: true);
                to.FormaPagamento = new FormaPagamentoEntity
                {
                    Cartao = from.ConvertTo<CartaoEntity>(skipConverters: true)
                };
                return to;
            });
        }
        public async Task ProcessarPagamento(CartaoParam cartao)
        {
            var pagamento = cartao.ConvertTo<PagamentoEntity>();

            
        }
    }
}