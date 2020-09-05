using Devboost.Pagamentos.Domain.Interfaces.Commands;
using Devboost.Pagamentos.Domain.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PagamentoController : Controller
    {
        private readonly IPagamentoCommand _pagamentoCommand;

        public PagamentoController(IPagamentoCommand pagamentoCommand)
        {
            _pagamentoCommand = pagamentoCommand;
        }

        /// <summary>
        /// Criar um pedido
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/pedidos
        ///     {
        ///        "peso": 10000
        ///     }
        ///
        /// </remarks>        
        /// <param name="command"></param>  
        [HttpPost("cartao")]
        public async Task<IActionResult> Post([FromBody] CartaoParam cartao)
        {
            try
            {
                await _pagamentoCommand.ProcessarPagamento(cartao);

                return Ok("Pagamento realizado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}