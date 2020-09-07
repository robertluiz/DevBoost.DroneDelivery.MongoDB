using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        Task Add(TEntity obj);
        Task AddUsingRef(TEntity obj);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetByID(Guid id);

        Task Update(TEntity obj);
        
    }
}