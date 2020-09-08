using Devboost.Pagamentos.Domain.Enums;
using ServiceStack.DataAnnotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Devboost.Pagamentos.Repository.Model
{
    [Alias("Pagamento")]
    public class Pagamento
    {
        [AutoId]
        [PrimaryKey]
        [NotNull]
        public Guid? Id { get; set; }

        public Guid IdPedido { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
        public float Valor { get; set; }

        [Reference]
        public virtual Cartao Cartao { get; set; }
        
        [ForeignKey(typeof(Cartao))]
        public Guid? CartaoId { get; set; }

        [Reference]
        public virtual Boleto Boleto { get; set; }

        [ForeignKey(typeof(Boleto))]
        public Guid? BoletoId { get; set; }
    }
}