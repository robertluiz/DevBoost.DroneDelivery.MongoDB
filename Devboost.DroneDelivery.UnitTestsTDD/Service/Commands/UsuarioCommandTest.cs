using System.Threading.Tasks;
using AutoBogus;
using AutoMoqCore;
using Devboost.DroneDelivery.Domain.Entities;
using Devboost.DroneDelivery.Domain.Enums;
using Devboost.DroneDelivery.Domain.Interfaces.Repository;
using Devboost.DroneDelivery.Domain.Params;
using Devboost.DroneDelivery.DomainService.Commands;
using Moq;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsTDD.Service.Commands
{
    public class UsuarioCommandTest
    {

        [Fact(DisplayName = "CriarUsuario")]
        [Trait("UsuarioCommand", "Command Tests")]
        public async Task Criar_test()
        {
            var mocker = new AutoMoqer();
            var baseCommandMock = mocker.Create<UsuarioCommand>();

            var param = new AutoFaker<UsuarioParam>()
                .RuleFor(fake => fake.Role, fake => fake.PickRandom<RoleEnum>().ToString())
                .Generate();

            var usuarioRepoMock = mocker.GetMock<IUsuariosRepository>();

            usuarioRepoMock.Setup(u=> u.Inserir(It.IsAny<UsuarioEntity>())).Returns(Task.Factory.StartNew(()=> string.Empty)).Verifiable();

            await baseCommandMock.Criar(param);


            usuarioRepoMock.Verify(u=> u.Inserir(It.IsAny<UsuarioEntity>()), Times.Once);

        }

    }
}