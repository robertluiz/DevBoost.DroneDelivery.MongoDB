using System;
using System.ComponentModel.DataAnnotations;
namespace Devboost.DroneDelivery.Domain.Params
{
    public class PedidoParam
    {
        [Required]
        public int Peso { get; set; }        
        public string Login { get; set; }
        public float DistanciaEmMetros { get; set; }
        //[Required]
        //public double Latitude { get; set; }
        //[Required]
        //public double Longitude { get; set; }
        [Required]
        public DateTime DataHora { get; set; }        
    }
}