using Devboost.Pagamentos.Domain.Params;
using Microsoft.AspNetCore.Mvc;

namespace Devboost.Pagamentos.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        
        [HttpPost("cartao")]
        public void Post([FromBody] CartaoParam cartao)
        {

        }
    }
}