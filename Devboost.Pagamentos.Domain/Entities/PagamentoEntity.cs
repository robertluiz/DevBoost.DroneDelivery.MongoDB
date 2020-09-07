using System;
using Devboost.Pagamentos.Domain.Enums;
using Devboost.Pagamentos.Domain.Interfaces.Entity;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class PagamentoEntity : IEntity
    {
        public Guid? Id { get; set; }
        public Guid IdPedido { get; set; }
        public string Descricao { get; set; }
        public float Valor { get; set; }
        public Guid FormaPagamentoID { get; set; }

        public StatusPagamentoEnum StatusPagamento { get; set; }
        public FormaPagamentoEntity FormaPagamento { get; set; }

        public string[] Validar()
        {
            throw new NotImplementedException();
        }
    }
}