using com.project.pagapoco.app.webmvc.Services.Imp;
using com.project.pagapoco.core.entities.Dto.Request;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.app.webmvc.Services
{
    public class AuthService : IAuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {

            try
            {

                Console.WriteLine("Intentando acceder");
                var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"Error en el login: {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<AuthResponse>();

            } catch (Exception ex)
            {
                Console.WriteLine("Error..");
                throw new ApplicationException("Error de inicio de sesión", ex);
            }

        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {

            try
            {

                var response = await _httpClient.PostAsJsonAsync("/api/Auth/register", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"Error al registrar: {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<AuthResponse>();

            } catch(Exception ex)
            {
                throw new ApplicationException("Error al registrar", ex);
            }
        
        }
    }
}
