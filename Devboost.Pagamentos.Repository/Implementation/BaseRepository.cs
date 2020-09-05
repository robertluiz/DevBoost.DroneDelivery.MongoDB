using Devboost.Pagamentos.Repository.Model;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Devboost.Pagamentos.Domain.Interfaces.Repository;

namespace Devboost.Pagamentos.Repository.Implementation
{
    public class BaseRepository<TEntity,TModel> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task Add(TEntity obj)
        {
            var model = obj.ConvertTo<TModel>();

            _connection.CreateTableIfNotExists<TModel>();            
            await _connection.InsertAsync(model);
        }

        public async Task AddUsingRef(TEntity obj)
        {
            var model = obj.ConvertTo<TModel>();

            _connection.CreateTableIfNotExists<TModel>();
            await _connection.SaveAsync(model, references: true);            
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            _connection.CreateTableIfNotExists<TModel>();

            var list = await _connection.SelectAsync<TModel>();

            return list.ConvertTo<List<TEntity>>();
        }

        public async Task<TEntity> GetByID(Guid id)
        {
            _connection.CreateTableIfNotExists<TModel>();           

            var p = await _connection.SingleAsync<TModel>(new { Id = id });

            return p.ConvertTo<TEntity>();
        }

        public async Task Update(TEntity obj)
        {
            var model = obj.ConvertTo<TModel>();

            _connection.CreateTableIfNotExists<TModel>();
            await _connection.UpdateAsync(model);
        }
    }
}