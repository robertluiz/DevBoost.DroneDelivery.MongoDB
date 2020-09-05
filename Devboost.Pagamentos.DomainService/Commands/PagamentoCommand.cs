using System;
//using System.ComponentModel.DataAnnotations;

namespace Devboost.Pagamentos.DomainService.Commands
{
    public class PagamentoCommand
    {
        //[Required]
        public Guid IdPedido { get; set; }
        //[Required]
        public float Valor { get; set; }

    }
}