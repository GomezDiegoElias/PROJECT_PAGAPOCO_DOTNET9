using System.Net.Http.Headers;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.app.webmvc.Services.Imp;
using com.project.pagapoco.core.entities.Dto.Response;
using System.Net;
using Microsoft.AspNetCore.Authentication;

namespace com.project.pagapoco.app.webmvc.Services
{
    public class PublicationService : IPublicationService
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PublicationService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaginatedResponse<PublicationResponse>> GetPublications(int pageIndex, int pageSize)
        {
           
            var token = _httpContextAccessor.HttpContext?.User?.FindFirst("JWT")?.Value;

            if (string.IsNullOrEmpty(token))
            {
                throw new ApplicationException("Usuario no autenticado");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Publication?pageIndex={pageIndex}&pageSize={pageSize}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<PaginatedResponse<PublicationResponse>>>();
                return content?.Data ?? new PaginatedResponse<PublicationResponse> { Items = new List<PublicationResponse>(), TotalCount = 0 };
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                throw new UnauthorizedAccessException("Sesión expirada, por favor vuelva a iniciar sesión");
            }

            throw new ApplicationException($"Error al obtener publicaciones: {response.StatusCode}");

        }
    }
}
