using System.Collections.Generic;

namespace Devboost.Pagamentos.Domain.Interfaces.Entity
{
    public interface IEntity
    {
        List<string> Validar();
    }
}