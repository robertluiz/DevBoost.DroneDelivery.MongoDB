using Devboost.Pagamentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ServiceStack.DataAnnotations;

namespace Devboost.Pagamentos.Repository.Model
{
    [Table("dbo.Cartao")]
	public class Cartao
	{
        public Guid Id { get; set; }
		public string Nome { get; set; }
		public PagamentoBandeiraEnum Bandeira { get; set; }
		public string NumeroCartao { get; set; }
		public DateTime DataValidade { get; set; }
		public string CodSeguranca { get; set; }
		public TipoCartaoEnum Tipo { get; set; }
	}
}
