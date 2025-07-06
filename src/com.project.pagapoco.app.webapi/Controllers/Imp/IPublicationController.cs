using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers.Imp
{
    public interface IPublicationController
    {
        Task<ActionResult<ApiResponse<PaginatedResponse<PublicationResponse>>>> ListAllPublications(int pageIndex, int pageSize);
        Task<ActionResult<ApiResponse<PublicationResponse>>> SearchPublicationByCode(long code);
        Task<ActionResult<ApiResponse<PublicationResponse>>> CreatePublication(PublicationCreatedRequest request);
        Task<ActionResult<ApiResponse<PublicationResponse>>> EditPublication(long codePublication, PublicationUpdatedRequest request);
        Task<ActionResult<ApiResponse<object>>> RemovePublication(long code);
    }
}
