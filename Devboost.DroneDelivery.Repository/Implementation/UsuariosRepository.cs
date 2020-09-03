using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Repository.Models;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Devboost.DroneDelivery.Repository.Implementation
{
    public class UsuariosRepository : IUsuariosRepository, IDisposable
    {
        private readonly IDbConnection _connection;

        public UsuariosRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task Inserir(UsuarioEntity user)
        {
            var model = user.ConvertTo<Usuario>();
            

            _connection.CreateTableIfNotExists<Usuario>();
            await _connection.InsertAsync(model);

        }

        public async Task<List<UsuarioEntity>> GetAll()
        {
            

            var list = await _connection.SelectAsync<Usuario>();

            return list.ConvertTo<List<UsuarioEntity>>();

        }

        public async Task<UsuarioEntity> GetSingleById(Guid id)
        {
            
            _connection.CreateTableIfNotExists<Usuario>();
            var u = await _connection.SingleAsync<Usuario>(
                u =>
                    u.Id == id);

            return u.ConvertTo<UsuarioEntity>();

        }

        public async Task<UsuarioEntity> GetSingleByLogin(string login)
        {
            
            _connection.CreateTableIfNotExists<Usuario>();
            var u = await _connection.SingleAsync<Usuario>(
                u =>
                    u.Login.ToLower() == login.ToLower());

            return u.ConvertTo<UsuarioEntity>();

        }

        public async Task<UsuarioEntity> GetUsuarioByLoginSenha(UsuarioEntity usuario)
        {
            
            if (_connection.CreateTableIfNotExists<Usuario>())
            {
                await _connection.InsertAllAsync(SeedUsuario());
            }
            var retornoUsuario = await _connection.SingleAsync<Usuario>(
                u =>
                    u.Login.ToLower() == usuario.Login.ToLower() && u.Senha == usuario.Senha);

            return retornoUsuario.ConvertTo<UsuarioEntity>();
        }

        private static List<Usuario> SeedUsuario()
        {
            return new List<Usuario>
            {
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    Login = "fulano",
                    Nome = "Fulano da Silva Ramos",
                    Role = RoleEnum.Comprador,
                    Senha = "123456#",
                    Latitude = -23.592806,
                    Longitude = -46.674925,
                    DataCadastro = DateTime.Now
                },
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    Login = "ciclano",
                    Nome = "Ciclano da Silva",
                    Role = RoleEnum.Administrador,
                    Senha = "123456#",
                    Latitude = -23.592806,
                    Longitude = -46.674925,
                    DataCadastro = DateTime.Now
                },
            };
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UsuariosRepository()
        {
            Dispose(false);
        }
    }
}