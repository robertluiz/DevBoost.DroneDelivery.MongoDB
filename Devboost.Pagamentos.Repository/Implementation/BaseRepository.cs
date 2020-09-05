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
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task Add(T obj)
        {
            var model = obj.ConvertTo<Pagamento>();

            _connection.CreateTableIfNotExists<Pagamento>();
            await _connection.InsertAsync(model);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            _connection.CreateTableIfNotExists<Pagamento>();

            var list = await _connection.SelectAsync<Pagamento>();

            return list.ConvertTo<List<T>>();
        }

        public async Task<T> GetByID(Guid id)
        {
            _connection.CreateTableIfNotExists<Pagamento>();
            var p = await _connection.SingleAsync<Pagamento>(s => s.Id == id);

            return p.ConvertTo<T>();
        }

        public async Task Remove(T obj)
        {
            throw new NotImplementedException();
        }

        public async Task Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}