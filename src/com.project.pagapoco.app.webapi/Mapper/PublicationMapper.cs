using com.project.pagapoco.app.webapi.Dto.Request;
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

        // Mapeo de PublicationCreatedRequest a Publication
        public static Publication PublicationCreatedRequestToPublication(PublicationCreatedRequest request)
        {
            return new Publication(
                request.Code,
                request.Title,
                request.Description ?? string.Empty,
                request.Price,
                request.Brand,
                request.Model,
                request.Year,
                request.UserId
            );
        }

        // Mapeo de PublicationUpdatedRequest a Publication
        public static Publication PublicationUpdatedRequestToPublication(PublicationUpdatedRequest request)
        {
            return new Publication
            {
                Title = request.Title,
                Description = request.Description ?? string.Empty,
                Price = request.Price,
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year
            };
        }

    }
}
