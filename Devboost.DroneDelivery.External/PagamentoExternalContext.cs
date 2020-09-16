using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.External;
using Devboost.DroneDelivery.Domain.Params;
using Devboost.DroneDelivery.Domain.VOs;
using Flurl.Http;

namespace Devboost.DroneDelivery.External
{
    public class PagamentoExternalContext : IPagamentoExternalContext

    {
        private readonly ExternalConfigVO _config;

        public PagamentoExternalContext(ExternalConfigVO config)
        {
            _config = config;
        }

        public async Task EfetuarPagamentoCartao(PagamentoCartaoParam pagamento)
        {
            var request = new PagamentoDTO
            {
                IdPedido = pagamento.IdPedido,
                Valor = pagamento.Valor,
                Bandeira = pagamento.BandeiraCartao,
                Nome = pagamento.NomeCartao,
                NumeroCartao = pagamento.NumeroCartao,
                DataValidade = pagamento.DataValidadeCartao,
                CodSeguranca = pagamento.CodSegurancaCartao,
                Tipo = pagamento.TipoCartao
            };

            var url = $"{_config.UrlPagamento}/pagamento/cartao";

            await url.PostJsonAsync(request)
                .ConfigureAwait(false);

        }
    }
}