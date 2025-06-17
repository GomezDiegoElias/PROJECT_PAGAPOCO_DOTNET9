using com.project.pagapoco.core.business;
using com.project.pagapoco.core.data;
using com.project.pagapoco.core.entities;
using Moq;

namespace com.project.pagapoco.tests
{
    public class UserServiceTests
    {

        // Uso de Moq para crear un mock del repositorio de usuarios
        // Se hace uso de la interfaz UserRepository no de la implementación concreta
        private readonly Mock<UserRepository> _mockUserRepository;

        // Instancia del servicio de usuario
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<UserRepository>(MockBehavior.Strict);
            _userService = new UserServiceImp(_mockUserRepository.Object);
        }

        [Fact]
        public void DebeBuscarUnUsuarioPorSuId()
        {

            // Arrange / Preparar

            // Crea un usuario de ejemplo que será devuelto por el mock
            var idUsuario = 1;
            var usuarioEsperado = new User
            {
                Id = idUsuario,
                FirstName = "Lionel",
                LastName = "Messi",
                Email = "correodemessi123@gmail.com",
                Password = "12356"
            };

            // Configura el mock para que devuelva el usuario esperado cuando se llame a FindById
            _mockUserRepository
                .Setup(x => x.FindById(idUsuario))
                .Returns(usuarioEsperado);

            // Act / Ejecutar
            var usuarioObtenido = _userService.findById(idUsuario);

            // Assert / Verificar
            Assert.NotNull(usuarioObtenido);
            Assert.Equal(usuarioEsperado.Id, usuarioObtenido.Id);
            Assert.Equal(usuarioEsperado.FirstName, usuarioObtenido.FirstName);

            // Verifica que el método FindById fue llamado una vez con el id correcto
            _mockUserRepository.Verify(x => x.FindById(idUsuario), Times.Once);


        }
    }
}
