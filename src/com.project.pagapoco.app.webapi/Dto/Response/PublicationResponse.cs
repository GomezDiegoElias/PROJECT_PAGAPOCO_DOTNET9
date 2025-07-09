namespace com.project.pagapoco.app.webapi.Dto.Response
{
    public record PublicationResponse(
        long id,
        long CodePublication,
        string Title,
        string Description,
        decimal Price,
        string Brand,
        string Model,
        string Year,
        int UserId
    );
}
