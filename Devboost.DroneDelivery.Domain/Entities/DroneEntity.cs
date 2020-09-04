using System;
using System.Diagnostics.CodeAnalysis;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class DroneEntity
    {
        public Guid Id { get; set; }
        public int Capacidade { get; set; }
        public int Velocidade { get; set; }
        public int Autonomia { get; set; }
        
        public int Carga { get; set; }
        public DroneStatus Status { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public readonly double AUTONOMIA_MAXIMA = 35;
        public readonly double TEMPO_RECARGA_MINUTOS = 60;
        public readonly double AUTONOMIA_RECARGA = 95;
        

    }
}