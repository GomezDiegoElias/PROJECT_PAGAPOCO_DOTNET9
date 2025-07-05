using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.app.webapi.Mapper
{
    public static class PublicationMapper
    {

        // Mapeo de la entidad Publication a PublicationResponse
        public static PublicationResponse PublicationToPublicationResponse(Publication publication)
        {
            return new PublicationResponse(
                publication.Id,
                publication.CodePublication,
                publication.Title,
                publication.Description ?? string.Empty,
                publication.Price,
                publication.Brand,
                publication.Model,
                publication.Year.ToString(),
                publication.UserId
            );
        }

    }
}
