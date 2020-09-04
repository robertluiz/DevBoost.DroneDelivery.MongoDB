using Devboost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ConsultaUsuariosDTO
    {        
        public string Situacao { get; set; }
        public List<PedidoEntity> Pedidos { get; set; }        
    }
}