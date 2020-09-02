using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.DomainService
{
    public class EntregaCommand : IEntregaCommand
    {
        private readonly IDroneCommand _droneCommand;        

        public EntregaCommand(IDroneCommand droneCommand)
        {
            _droneCommand = droneCommand;            
        }

        public async Task<bool> Inicia()
        {
            try
            {
                await _droneCommand.LiberaDrone();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }        
    }
}