namespace com.project.pagapoco.app.webapi.Dto.Response
{
    public record UserResponse(
        int Id,
        string FirstName, 
        string LastName, 
        string Email
    );
}
