using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Devboost.Pagamentos.Domain.Enums;

namespace Devboost.Pagamentos.Repository.Model
{
	[Table("dbo.Pagamento")]
	public class Pagamento
	{

		public Guid Id { get; set; }        

		public Guid IdPedido { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
		public float Valor { get; set; }
        [Reference]
        public virtual FormaPagamento FormaPagamento { get; set; }

        [References(typeof(FormaPagamento))]
		public Guid FormaPagamentoId { get; set; }
		
	}
}
