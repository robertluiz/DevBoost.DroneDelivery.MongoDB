
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Params;

namespace Devboost.Pagamentos.Domain.Interfaces.External.Context
{
    public interface IDeliveryExternalContext
    {
        Task AtualizaStatusPagamento(DeliveryExternalParam deliverypParam);
    }
}