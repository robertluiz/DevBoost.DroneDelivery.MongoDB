using System;

namespace Devboost.Pagamentos.Kafka.Consumer.Model
{
    public class PagamentoStatus
    {
        public Guid IdPedido { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
    }
}