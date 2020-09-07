using Devboost.Pagamentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devboost.Pagamentos.Repository.Model
{
	public class Cartao
	{
		public Cartao() 
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public string Nome { get; set; }
		public PagamentoBandeiraEnum Bandeira { get; set; }
		public string NumeroCartao { get; set; }
		public DateTime DataValidade { get; set; }
		public string CodSeguranca { get; set; }
		public TipoCartaoEnum Tipo { get; set; }
	}
}
