using System;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class FormaPagamentoEntity
    {
        public Guid Id { get; set; }        
        public CartaoEntity Cartao { get; set; }
        //public BoletoEntity Boleto { get; set; }
    }
}