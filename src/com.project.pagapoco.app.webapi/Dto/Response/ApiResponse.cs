namespace com.project.pagapoco.app.webapi.Dto.Response
{
    // Uso del constructor primario
    public class ApiResponse<T>(bool success, string? message, T? data)
    {

        public bool Success { get; set; } = success; // string
        public string? Message { get; set; } = message;
        public T? Data { get; set; } = data;

    }
}
