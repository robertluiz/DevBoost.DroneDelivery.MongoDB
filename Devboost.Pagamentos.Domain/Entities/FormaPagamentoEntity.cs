using System;
using Devboost.Pagamentos.Domain.Interfaces.Entity;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class FormaPagamentoEntity : IEntity
    {
        public Guid? Id { get; set; }
        
        public Guid CartaoID { get; set; }
        public CartaoEntity Cartao { get; set; }
        //public BoletoEntity Boleto { get; set; }
        public string[] Validar()
        {
            throw new NotImplementedException();
        }
    }
}