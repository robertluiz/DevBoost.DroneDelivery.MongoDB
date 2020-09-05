using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Interfaces.External;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Domain.Params;
using ServiceStack;


namespace Devboost.Pagamentos.DomainService.Commands
{
    public class PagamentoCommand: IPagamentoCommand
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IGatewayService _gatewayService;
        private readonly IDeliveryService _deliveryService;

        public PagamentoCommand(IPagamentoRepository pagamentoRepository, IGatewayService gatewayService, IDeliveryService deliveryService)
        {
            _pagamentoRepository = pagamentoRepository;
            _gatewayService = gatewayService;
            _deliveryService = deliveryService;
        }

        public async Task<string[]> ProcessarPagamento(CartaoParam cartao)
        {
            var pagamento = cartao.ConvertTo<PagamentoEntity>();
            var erros = pagamento.Validar();

            if (erros.Length > 0) return erros;

<<<<<<< HEAD
            await _pagamentoRepository.Inserir(pagamento);
            
            var confirmacaoPagamento = await _gatewayService.EfetuaPagamento(pagamento);
            await _deliveryService.SinalizaStatusPagamento(confirmacaoPagamento);

=======
            await _pagamentoRepository.Add(pagamento);
>>>>>>> 3f519954827e8b1067897486e6ba18adc33f8e5c

            return erros;
        }
    }
}