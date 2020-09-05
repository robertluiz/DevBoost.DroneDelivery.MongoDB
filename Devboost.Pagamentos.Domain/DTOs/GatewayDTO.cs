using System;
using Devboost.Pagamentos.Domain.Enums;

namespace Devboost.Pagamentos.Domain.DTOs
{
    public class GatewayDTO
    {
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public string Mensagem { get; set; }
        public Guid IdPedido { get; set; }

    }
}