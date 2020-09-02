using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/drone")]
    [ApiController]
    public class DroneController : Controller
    {
        private readonly IDroneCommand _droneCommand;
        private readonly IDroneQuery _droneQuery;

        public DroneController(IDroneCommand droneCommand, IDroneQuery droneQuery)
        {
            _droneCommand = droneCommand;
            _droneQuery = droneQuery;
        }

        [HttpGet("situacao")]
        [AllowAnonymous]
        public async Task<IActionResult> SituacaoDrone()
        {
            try
            {
                var lista = await _droneQuery.ConsultaDrone();
                if (lista.Count.Equals(0)) return NotFound();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}