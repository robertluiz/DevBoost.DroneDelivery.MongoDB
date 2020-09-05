using System;
using System.Collections.Generic;
using System.Text;

namespace Devboost.Pagamentos.Repository.Model
{
	public class Cartao
	{
		public string Bandeira { get; set; }
		public string NumeroCartao { get; set; }
		public DateTime DataValidade { get; set; }
		public string CodSeguranca { get; set; }
		public string Tipo { get; set; }
	}
}
