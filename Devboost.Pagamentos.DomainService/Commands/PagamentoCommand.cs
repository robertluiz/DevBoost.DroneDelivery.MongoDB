using System.Collections.Generic;
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Enums;
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
        private readonly IGatewayExternalService _gatewayExternalService;
        private readonly IDeliveryExternalService _deliveryExternalService;

        public PagamentoCommand(IPagamentoRepository pagamentoRepository, IGatewayExternalService gatewayExternalService, IDeliveryExternalService deliveryExternalService)
        {
            _pagamentoRepository = pagamentoRepository;
            _gatewayExternalService = gatewayExternalService;
            _deliveryExternalService = deliveryExternalService;
        }

        public async Task<List<string>> ProcessarPagamento(CartaoParam cartao)
        {
            var pagamento = cartao.ConvertTo<PagamentoEntity>();
            var erros = pagamento.Validar();

            if (erros.Count > 0) return erros;

            pagamento.StatusPagamento = StatusPagamentoEnum.Pendente;
            await _pagamentoRepository.AddUsingRef(pagamento);

            var confirmacaoPagamento = await _gatewayExternalService.EfetuaPagamento(pagamento);
            await _deliveryExternalService.SinalizaStatusPagamento(confirmacaoPagamento);
            
            pagamento.StatusPagamento = confirmacaoPagamento.StatusPagamento;
            await _pagamentoRepository.Update(pagamento);

            return erros;
        }
    }
}