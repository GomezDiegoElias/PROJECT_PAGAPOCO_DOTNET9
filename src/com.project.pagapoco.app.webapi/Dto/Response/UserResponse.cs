namespace com.project.pagapoco.app.webapi.Dto.Response
{
    public record UserResponse(
        //Guid Id,
        int Id,
        long Dni,
        string FirstName, 
        string LastName, 
        string Email
    );
}
