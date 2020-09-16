using System;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    public class PagamentoDTO
    {
        public Guid IdPedido { get; set; }

        public float Valor { get; set; }
        public PagamentoBandeiraEnum Bandeira { get; set; }
        public string Nome { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CodSeguranca { get; set; }
        public TipoCartaoEnum Tipo { get; set; }
    }
}