using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T>
    {
        Task Add(T obj);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetByID(Guid id);


        Task Remove(T obj);

        Task Update(T obj);
        
    }
}