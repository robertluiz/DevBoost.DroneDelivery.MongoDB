using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Devboost.Pagamentos.Repository.Model
{
	[Table("dbo.FormaPagamento")]
	public class FormaPagamento
	{
		public Guid Id { get; set; }

		[References(typeof(Cartao))]
		public Guid CartaoID { get; set; }
		
		[Reference]
		public Cartao Cartao { get; set; }
	}
}
