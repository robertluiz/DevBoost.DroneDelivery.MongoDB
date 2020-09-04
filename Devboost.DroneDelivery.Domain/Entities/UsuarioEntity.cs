using System;
using System.Diagnostics.CodeAnalysis;
using Devboost.DroneDelivery.Domain.Enums;

namespace Devboost.DroneDelivery.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class UsuarioEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public RoleEnum Role { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}