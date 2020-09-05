using System;
using System.Collections.Generic;
using System.Text;

namespace Devboost.Pagamentos.Repository.Model
{
	public class FormaPagamento
	{
		public Guid Id { get; set; }
		public Cartao Cartao { get; set; }
	}
}
