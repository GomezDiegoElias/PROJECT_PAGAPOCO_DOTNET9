namespace com.project.pagapoco.app.webapi.Dto.Request
{
    public record UserRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
