using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Commands;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Params;
using System;
using System.Threading.Tasks;
using ServiceStack;

namespace Devboost.DroneDelivery.DomainService.Commands
{
    public class UsuarioCommand : IUsuarioCommand
    {
        
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuarioCommand(IUsuariosRepository usuariosRepository)
        {            
            _usuariosRepository = usuariosRepository;
        }        

        public async Task Criar(UsuarioParam user)
        {

            var hasRole = Enum.TryParse<RoleEnum>(user.Role, true, out RoleEnum roleUser);

            if (!hasRole)
                throw new Exception("Perfil não encontrado!");


            var usuario = user.ConvertTo<UsuarioEntity>();
            usuario.DataCadastro = DateTime.Now;

            usuario.Role = roleUser;
            usuario.Id = Guid.NewGuid();

            await _usuariosRepository.Inserir(usuario);


        }
    }
}