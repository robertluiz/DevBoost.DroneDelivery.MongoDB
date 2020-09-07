using System;
using System.Collections.Generic;
using System.Linq;
using Devboost.Pagamentos.Domain.Interfaces.Entity;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class FormaPagamentoEntity : IEntity
    {
        public Guid Id { get; set; }
        public CartaoEntity Cartao { get; set; }
        //public BoletoEntity Boleto { get; set; }
        public List<string> Validar()
        {
            var result = new List<string>();

            var listErrosCartao = Cartao.Validar();

            if (listErrosCartao.Count() > 0)
                result.AddRange(listErrosCartao);

            return result;
        }
    }
}