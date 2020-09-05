using System;
using System.Collections.Generic;
using System.Text;

namespace Devboost.Pagamentos.Repository.Model
{
	public class Pagamento
	{
		public Guid Id { get; set; }
		public Guid IdPedido { get; set; }
		public float Valor { get; set; }
		public FormaPagamento FormaPagamento { get; set; }
	}
}
