using System;
using System.ComponentModel.DataAnnotations;

namespace Devboost.Pagamentos.Domain.Params
{
    public class PagamentoParam
    {
        [Required]
        public Guid IdPedido { get; set; }
        [Required]
        public float Valor { get; set; }

        public string Descricao { get; set; }
    }
}