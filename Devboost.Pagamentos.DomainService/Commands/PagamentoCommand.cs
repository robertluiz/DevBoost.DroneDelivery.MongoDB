using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Params;
using ServiceStack;


namespace Devboost.Pagamentos.DomainService.Commands
{
    public class PagamentoCommand: IPagamentoCommand
    {

        public async Task ProcessarPagamento(CartaoParam cartao)
        {
            var pagamento = cartao.ConvertTo<PagamentoEntity>();

            
        }
    }
}