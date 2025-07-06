using System.Text.Json;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities.Dto.Response;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using com.project.pagapoco.app.webmvc.Services.Imp;

namespace com.project.pagapoco.app.webmvc.Services
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
        public async Task<PaginatedResponse<UserResponse>> getUsers(int pageIndex, int pageSize)
        {
            var token = _httpContextAccessor.HttpContext?.User?.FindFirst("JWT")?.Value;

            if (string.IsNullOrEmpty(token))
            {
                throw new ApplicationException("Usuario no autenticado");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/User?pageIndex={pageIndex}&pageSize={pageSize}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<PaginatedResponse<UserResponse>>>();
                return content?.Data ?? new PaginatedResponse<UserResponse> { Items = new List<UserResponse>(), TotalCount = 0 };
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                throw new UnauthorizedAccessException("Sesión expirada, por favor vuelva a iniciar sesión");
            }

            throw new ApplicationException($"Error al obtener usuarios: {response.StatusCode}");
        }

    }
}
