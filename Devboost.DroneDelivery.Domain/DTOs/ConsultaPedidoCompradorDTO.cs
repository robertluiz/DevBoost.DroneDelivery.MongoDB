using System;
using System.Diagnostics.CodeAnalysis;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ConsultaPedidoCompradorDTO
    {
        public Guid PedidoId { get; set; }
        public int Peso { get; set; }        
        public DateTime? DataHora { get; set; }
        public string Status { get; set; }
        public Guid CompradorId { get; set; }
        public string NomeComprador { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanciaDaEntrega { get; set; }
    }
}