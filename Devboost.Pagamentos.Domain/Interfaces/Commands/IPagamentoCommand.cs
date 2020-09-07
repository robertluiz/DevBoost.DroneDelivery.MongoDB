using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Params;

namespace Devboost.Pagamentos.Domain.Interfaces.Commands
{
    public interface IPagamentoCommand
    {
        Task<List<string>> ProcessarPagamento(CartaoParam cartao);
    }
}