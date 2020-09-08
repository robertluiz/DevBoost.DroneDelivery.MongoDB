using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Queries;
using Devboost.DroneDelivery.Domain.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Devboost.DroneDelivery.Api.Controllers
{
    [Route("v1/usuario")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioCommand _usuarioCommand;
        private readonly IUsuarioQuery _usuarioQuery;

        public UsuarioController(IUsuarioCommand usuarioCommand, IUsuarioQuery usuarioQuery)
        {
            _usuarioCommand = usuarioCommand;
            _usuarioQuery = usuarioQuery;
        }

        [HttpPost("cadastrar")]        
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioParam user)
        {
            try
            {
                await _usuarioCommand.Criar(user);
                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }

        }

        [HttpGet("list")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _usuarioQuery.GetAll();
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