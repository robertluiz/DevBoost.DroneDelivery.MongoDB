using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Params;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.DomainService.Commands
{
    public class PagamentoCommand : IPagamentoCommand
    {
        public Task ProcessarPagamento(PagamentoParam p)
        {
            throw new System.NotImplementedException();
        }
    }
}