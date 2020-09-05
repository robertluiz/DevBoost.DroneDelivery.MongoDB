using Devboost.Pagamentos.Domain.Enums;
using System;
using Devboost.Pagamentos.Domain.Interfaces.Entity;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class CartaoEntity : IEntity
    {
        public PagamentoBandeiraEnum Bandeira { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CodSeguranca { get; set; }
        public TipoCartaoEnum Tipo { get; set; }
        public string[] Validar()
        {
            throw new NotImplementedException();
        }
    }
}