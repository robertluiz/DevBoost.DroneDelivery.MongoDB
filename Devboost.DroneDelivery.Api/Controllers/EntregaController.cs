using Devboost.DroneDelivery.Domain.DTOs;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/entrega")]
    [ApiController]
    public class EntregaController : Controller
    {
        private readonly IEntregaCommand _entregaCommand;

        public EntregaController(IEntregaCommand entregaCommand)
        {
            _entregaCommand = entregaCommand;
        }

        [HttpPost("Inicia")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult>  IniciaEntrega()
        {
            try
            {
                var resultado = await _entregaCommand.Inicia();

                if (!resultado)
                    return BadRequest("Erro ao iniciar a Entrega!");
                return Ok("Entrega iniciada!");
            }
            catch (Exception e)
            {
              return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }

        [HttpPost("Inicia/Pedido")]        
        public async Task<IActionResult> IniciaEntregaByPedido([FromBody] DeliveryExternalParam param)
        {
            try
            {
                await _entregaCommand.IniciaByPedido(param);
                return Ok("Entrega iniciada!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }
    }
}