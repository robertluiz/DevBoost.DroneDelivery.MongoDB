using System;

namespace devboost.DroneDelivery.Kafka.Consumer.Model
{
    public class Pedido
    {
        public int Peso { get; set; }
        public string Login { get; set; } = "fulano";
        public DateTime? DataHora { get; set; } = DateTime.Now;
    }
}