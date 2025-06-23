namespace com.project.pagapoco.app.webapi.Dto.Request
{
    public record UserUpdatedRequest
    (
        string FirstName,
        string LastName,
        string Email
        //string Password
    );
}
