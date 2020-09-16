using System;
using System.ComponentModel.DataAnnotations;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.Params
{
    public class PedidoParam
    {
        [Required]
        public int Peso { get; set; }        
        public string Login { get; set; }
        [Required]
        public DateTime DataHora { get; set; }
        [Required]
        public float Valor { get; set; }
        [Required]
        public string BandeiraCartao { get; set; }
        [Required]
        public string NomeCartao { get; set; }
        [Required]
        public string NumeroCartao { get; set; }
        [Required]
        public DateTime DataValidadeCartao { get; set; }
        [Required]
        public string CodSegurancaCartao { get; set; }
        [Required]
        public string TipoCartao { get; set; }
    }
}