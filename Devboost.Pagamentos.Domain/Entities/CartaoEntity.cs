using Devboost.Pagamentos.Domain.Enums;
using System;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class CartaoEntity
    {
        public Guid Id { get; set; }
        public PagamentoBandeiraEnum Bandeira { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CodSeguranca { get; set; }
        public TipoCartaoEnum Tipo { get; set; }
    }
}