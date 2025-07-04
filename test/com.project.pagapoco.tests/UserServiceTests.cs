using com.project.pagapoco.core.business.Service;
using com.project.pagapoco.core.business.Service.Imp;
using com.project.pagapoco.core.data.Repository.Imp;
using com.project.pagapoco.core.entities;
using Moq;

namespace com.project.pagapoco.tests
{
    public class UserServiceTests
    {

        // Uso de Moq para crear un mock del repositorio de usuarios
        // Se hace uso de la interfaz UserRepository no de la implementación concreta
        private readonly Mock<IUserRepository> _mockUserRepository;

        // Instancia del servicio de usuario
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task DebeBuscarUnUsuarioExistentePorSuDni()
        {

            // Arrange / Preparar

            // Crea un usuario de ejemplo que será devuelto por el mock
            //var idUser = 1;
            var dniUsuario = 44556622;
            var usuarioEsperado = new User
            {
                //Id = idUser,
                Dni = dniUsuario,
                FirstName = "Lionel",
                LastName = "Messi",
                Email = "correodemessi123@gmail.com",
                Password = "12356"
            };

            // Configura el mock para que devuelva el usuario esperado cuando se llame a FindByDni
            _mockUserRepository
                .Setup(x => x.FindByDni(dniUsuario))
                .ReturnsAsync(usuarioEsperado);

            // Act / Ejecutar
            var usuarioObtenido = await _userService.GetUserByDni(dniUsuario);

            // Assert / Verificar
            Assert.NotNull(usuarioObtenido);
            Assert.Equal(usuarioEsperado.Dni, usuarioObtenido.Dni);
            Assert.Equal(usuarioEsperado.FirstName, usuarioObtenido.FirstName);

            // Verifica que el método FindById fue llamado una vez con el id correcto
            _mockUserRepository.Verify(x => x.FindByDni(dniUsuario), Times.Once);

        }

        [Fact]
        public async Task DebeBuscarUnUsuarioQueNoExisteYLanzarErrorPorSuDni()
        {
            var dniIncorrecto = 11111111;
            _mockUserRepository
                .Setup(x => x.FindByDni(dniIncorrecto))
                .ReturnsAsync((User)null);

            var resultado = await _userService.GetUserByDni(dniIncorrecto);

            Assert.Null(resultado);
            _mockUserRepository.Verify(x => x.FindByDni(dniIncorrecto), Times.Once);
        }
    }
}
