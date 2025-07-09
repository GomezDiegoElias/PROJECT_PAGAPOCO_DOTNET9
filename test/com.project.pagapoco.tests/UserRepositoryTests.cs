using com.project.pagapoco.core.data;
using com.project.pagapoco.core.data.Repository;
using com.project.pagapoco.core.entities;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.tests
{
    public class UserRepositoryTests
    {

        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public UserRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_UserRepository")
                .Options;
        }

        [Fact]
        public async Task DebeBuscarUnUsuarioPorDni()
        {

            // Arrange
            using (var context = new AppDbContext(_dbContextOptions))
            {

                var dniUsuario = 44556622;
                var usuarioEsperado = new User
                {
                    Dni = dniUsuario,
                    FirstName = "Lionel",
                    LastName = "Messi"
                };

                context.Users.Add(usuarioEsperado);
                await context.SaveChangesAsync();

                var repository = new UserRepository(context);

                // Act
                var usuarioObtenido = await repository.FindByDni(dniUsuario);

                // Assert
                Assert.NotNull(usuarioObtenido);
                Assert.Equal(dniUsuario, usuarioObtenido.Dni);

            }

        }

        [Fact]
        public async Task DebeBuscarUnUsuarioQueNoExiste()
        {

            // Arrange
            using (var context = new AppDbContext(_dbContextOptions))
            {

                var repository = new UserRepository(context);

                // Act
                var resultado = await repository.FindByDni(55555555);

                // Assert
                Assert.Null(resultado);

            }

        }

    }
}
