namespace com.project.pagapoco.app.webapi.Dto.Response
{
    public record UserResponse(
        int Id,
        string Nombre, 
        string Apellido, 
        string Correo
    );
}
