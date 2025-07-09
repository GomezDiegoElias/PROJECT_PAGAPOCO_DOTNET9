namespace com.project.pagapoco.app.webapi.Dto.Request
{
    public record PublicationUpdatedRequest(
        string Title,
        string Description,
        decimal Price,
        string Brand,
        string Model,
        int Year
    );
}
