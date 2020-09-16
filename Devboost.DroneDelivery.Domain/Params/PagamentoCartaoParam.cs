using System;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.Params
{
    public class PagamentoCartaoParam
    {
        public Guid IdPedido { get; set; }

        public float Valor { get; set; }
        public PagamentoBandeiraEnum BandeiraCartao { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataValidadeCartao { get; set; }
        public string CodSegurancaCartao { get; set; }
        public TipoCartaoEnum TipoCartao { get; set; }

    }
}