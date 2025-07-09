using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace com.project.pagapoco.tests
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public UserControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task DebeBuscarUnUsuarioPorSuDni()
        {

            // Arrange
            var dniUsuario = 12345678;

            // Act
            var response = await _client.GetAsync($"/api/User/{dniUsuario}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        }

    }
    
}
