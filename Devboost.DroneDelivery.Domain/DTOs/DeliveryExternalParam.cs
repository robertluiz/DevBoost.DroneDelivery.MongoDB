using Devboost.DroneDelivery.Domain.Enums;
using System;

namespace Devboost.DroneDelivery.Domain.DTOs
{
    public class DeliveryExternalParam
    {
        public Guid IdPedido { get; set; }
        public StatusPagamentoEnum StatusPagamento { get; set; }
    }
}