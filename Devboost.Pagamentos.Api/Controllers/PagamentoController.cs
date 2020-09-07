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
        /// Processar Pagamento
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/pagamento
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
                var resultado = await _pagamentoCommand.ProcessarPagamento(cartao);

                if (resultado.Count > 0)
                    return BadRequest(string.Format("Dados para pagamento incorreto: \r\n{0}", string.Join("\r\n", resultado)));
                return Ok("Pagamento realizado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}