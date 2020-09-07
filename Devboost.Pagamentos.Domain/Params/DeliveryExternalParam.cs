using System;
using Devboost.Pagamentos.Domain.Enums;

namespace Devboost.Pagamentos.Domain.Params
{
    public class DeliveryExternalParam
    {
        public Guid IdPedido { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
    }
}