using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class PagamentoEntity : IEntity
    {
        public Guid? Id { get; set; }
        public Guid IdPedido { get; set; }
        public float Valor { get; set; }

        public StatusPagamentoEnum StatusPagamento { get; set; }
        public CartaoEntity Cartao { get; set; }

        public BoletoEntity Boleto { get; set; }

        public List<string> Validar()
        {
            var result = new List<string>();

            ValidaIdPedido(result);
            ValidaValor(result);

            if (Cartao != null)
                result.AddRange(Cartao.Validar());

            if (Boleto != null)
                result.AddRange(Boleto.Validar());

            return result;
        }

        public void ValidaIdPedido(List<string> listErros)
        {
            if (IdPedido == null)
                listErros.Add("IdPedido vazio!");
        }

        public void ValidaValor(List<string> listErros)
        {
            if (Valor <= 0)
                listErros.Add("Valor de pagamento não correspondente com o esperado que é maior que zero!");
        }
    }
}