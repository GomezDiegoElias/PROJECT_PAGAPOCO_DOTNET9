namespace com.project.pagapoco.app.webapi.Dto.Request
{
    public record PublicationCreatedRequest(
        long Code,
        string Title,
        string Description,
        decimal Price,
        string Brand,
        string Model,
        int Year,
        int UserId
    );
}
