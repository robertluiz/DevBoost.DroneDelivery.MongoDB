using ServiceStack.DataAnnotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Devboost.Pagamentos.Repository.Model
{
    [Alias("Boleto")]
    public class Boleto
    {        
        [AutoId]
        [PrimaryKey]
        [NotNull]
        public Guid? Id { get; set; }

        public string CodigoDeBarras { get; set; }
    }
}