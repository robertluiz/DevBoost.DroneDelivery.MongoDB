using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Domain.Params;
using ServiceStack;


namespace Devboost.Pagamentos.DomainService.Commands
{
    public class PagamentoCommand: IPagamentoCommand
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        public PagamentoCommand(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task ProcessarPagamento(CartaoParam cartao)
        {
            var pagamento = cartao.ConvertTo<PagamentoEntity>();

            await _pagamentoRepository.Add(pagamento);
        }
    }
}