namespace com.project.pagapoco.app.webapi.Dto.Response
{
    // Uso del constructor primario
    public class ApiResponse<T>(string success, string? message, T? data)
    {

        public string Success { get; set; } = success;
        public string? Message { get; set; } = message;
        public T? Data { get; set; } = data;

    }
}
