using Devboost.Pagamentos.Domain.Enums;
using ServiceStack.DataAnnotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Devboost.Pagamentos.Repository.Model
{
    [Alias("Cartao")]
    public class Cartao
    {
        [AutoId]
        [PrimaryKey]
        [NotNull]
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public PagamentoBandeiraEnum Bandeira { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CodSeguranca { get; set; }
        public TipoCartaoEnum Tipo { get; set; }
    }
}