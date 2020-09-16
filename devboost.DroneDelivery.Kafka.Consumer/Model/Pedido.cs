using System;
using Devboost.DroneDelivery.Domain.Enums;

namespace devboost.DroneDelivery.Kafka.Consumer.Model
{
    public class Pedido
    {
        public int Peso { get; set; }
        public string Login { get; set; } = "fulano";
        public DateTime? DataHora { get; set; } = DateTime.Now;

        public float Valor { get; set; }

        public string BandeiraCartao { get; set; }

        public string NomeCartao { get; set; }

        public string NumeroCartao { get; set; }
 
        public DateTime DataValidadeCartao { get; set; }

        public string CodSegurancaCartao { get; set; }

        public string TipoCartao { get; set; }
    }
}