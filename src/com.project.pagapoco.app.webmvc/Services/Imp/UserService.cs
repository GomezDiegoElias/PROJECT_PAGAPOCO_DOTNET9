using System.Text.Json;
using com.project.pagapoco.app.webapi.Dto.Response;
using Microsoft.AspNetCore.Authentication;

namespace com.project.pagapoco.app.webmvc.Services.Imp
{
    public class UserService : IUserService
    {

        //private readonly HttpClient _httpClient;

        //public UserService(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<UserResponse>> getUsers()
        {

            var token = _httpContextAccessor.HttpContext?.User?.FindFirst("JWT")?.Value;

            if (string.IsNullOrEmpty(token))
            {
                throw new ApplicationException("Usuario no autenticado");
            }

            // Crear request con el token de autorización
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/User");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Enviar la solicitud
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<List<UserResponse>>>();
                return content?.Data ?? new List<UserResponse>();
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Cerrar sesión si el token es inválido
                await _httpContextAccessor.HttpContext.SignOutAsync();
                throw new UnauthorizedAccessException("Sesión expirada, por favor vuelva a iniciar sesión");
            }

            throw new ApplicationException($"Error al obtener usuarios: {response.StatusCode}");

        }

        public Task<List<UserResponse>> getUsers(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
