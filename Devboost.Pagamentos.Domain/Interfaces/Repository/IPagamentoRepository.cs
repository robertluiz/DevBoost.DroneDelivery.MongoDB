using Devboost.Pagamentos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Domain.Interfaces.Repository
{
	public interface IPagamentoRepository
	{
		Task Inserir(PagamentoEntity pagamento);
		Task<PagamentoEntity> RetornoPagamento(Guid idPedido);
	}
}
