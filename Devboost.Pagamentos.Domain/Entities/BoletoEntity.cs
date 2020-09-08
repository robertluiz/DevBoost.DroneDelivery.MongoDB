using Devboost.Pagamentos.Domain.Interfaces.Entity;
using System;
using System.Collections.Generic;

namespace Devboost.Pagamentos.Domain.Entities
{
    public class BoletoEntity : IEntity
    {
        public Guid? Id { get; set; }
        public string CodigoDeBarras { get; set; }
        public DateTime DataValidade { get; set; }

        public List<string> Validar()
        {
            throw new NotImplementedException();
        }

        #region ValidaValores
        //NotImplemented
        #endregion ValidaValores
    }
}