using Devboost.DroneDelivery.Domain.Enums;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


namespace Devboost.DroneDelivery.Repository.Models
{

    public class Pedido
    {
        public Pedido()
        {
            Id = Guid.NewGuid();
            DataHora = DateTime.Now;
        }

        [BsonElement("id")]
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("peso")]
        public int Peso { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("dataHora")]
        public DateTime? DataHora { get; set; }

        [BsonElement("distanciaDaEntrega")]

        public double DistanciaDaEntrega { get; set; }

        [BsonElement("droneId")]
        public Guid DroneId { get; set; }

        [BsonElement("compradorId")]
        public Guid CompradorId { get; set; }

        [BsonElement("bandeiraCartao")]
        public PagamentoBandeiraEnum BandeiraCartao { get; set; }

        [BsonElement("nomeCartao")]
        public string NomeCartao { get; set; }

        [BsonElement("numeroCartao")]
        public string NumeroCartao { get; set; }

        [BsonElement("dataValidadeCartao")]
        public DateTime DataValidadeCartao { get; set; }

        [BsonElement("codSegurancaCartao")]
        public string CodSegurancaCartao { get; set; }

        [BsonElement("tipoCartao")]
        public TipoCartaoEnum TipoCartao { get; set; }

        [BsonElement("statusPagamento")]
        public StatusPagamentoEnum StatusPagamento { get; set; }
    }
}