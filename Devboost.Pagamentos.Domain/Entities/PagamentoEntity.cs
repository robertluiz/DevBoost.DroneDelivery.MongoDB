using System;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class PagamentoEntity
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public float Valor { get; set; }
        public FormaPagamentoEntity FormaPagamento { get; set; }        
    }
}