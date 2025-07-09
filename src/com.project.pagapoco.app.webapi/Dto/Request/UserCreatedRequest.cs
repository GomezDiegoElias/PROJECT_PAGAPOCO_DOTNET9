namespace com.project.pagapoco.app.webapi.Dto.Request
{
    public record UserCreatedRequest
    (
        long Dni,
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
