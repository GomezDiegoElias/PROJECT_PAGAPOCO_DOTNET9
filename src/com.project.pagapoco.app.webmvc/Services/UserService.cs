using System.Text.Json;
using com.project.pagapoco.app.webapi.Dto.Response;

namespace com.project.pagapoco.app.webmvc.Services
{
    public class UserService : IUserService
    {

        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserResponse>> getUsers()
        {

            //Console.WriteLine("Llamada a la api");

            //var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<UserResponse>>>("/api/User");

            //Console.WriteLine($"Success: {response?.Success}");
            //Console.WriteLine($"Data Type: {response?.Data?.GetType().Name}");

            //if (response?.Success == true && response.Data != null )
            //{
            //    Console.WriteLine("Obtencion de usuarios");
            //    return response.Data;
            //}

            //Console.WriteLine("No hay datos o la respuesta no fue exitosa");
            //return new List<UserResponse>();

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<UserResponse>>>("/api/User");

            if (response?.Success == true && response.Data != null)
            {
                return response.Data;
            }

            return new List<UserResponse>();

        }

        public Task<List<UserResponse>> getUsers(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
