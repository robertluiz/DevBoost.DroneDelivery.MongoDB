using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Params;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Domain.Interfaces.Commands
{
    public interface IUsuarioCommand
    {
        Task Criar(UsuarioParam user);
    }
}