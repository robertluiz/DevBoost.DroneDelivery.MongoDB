using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Params;

namespace Devboost.Pagamentos.Domain.Interfaces.Commands
{
    public interface IPagamentoCommand
    {
        Task ProcessarPagamento(CartaoParam cartao);
    }
}