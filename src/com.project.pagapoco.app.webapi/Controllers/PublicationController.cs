﻿using com.project.pagapoco.app.webapi.Controllers.Imp;
using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.app.webapi.Mapper;
using com.project.pagapoco.core.business.Service.Imp;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class PublicationController : Controller, IPublicationController
    {

        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<PublicationResponse>>>> ListAllPublications(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {

            var paginatedPublications = await _publicationService.GetAllPublications(pageIndex, pageSize);

            var publicationsDtos = paginatedPublications.Items
                .Select(publication => PublicationMapper.PublicationToPublicationResponse(publication))
                .ToList();

            var paginatedResponse = new PaginatedResponse<PublicationResponse>
            {
                Items = publicationsDtos,
                TotalCount = paginatedPublications.TotalCount
            };

            return Ok(new ApiResponse<PaginatedResponse<PublicationResponse>>(
                true,
                "Publications retrieved successfully",
                paginatedResponse
            ));

        }

        [HttpGet("{code}")]
        public async Task<ActionResult<ApiResponse<PublicationResponse>>> SearchPublicationByCode(long code)
        {
            
            var publication = await _publicationService.GetPublicationByCode(code);

            if (publication == null) 
                return NotFound(new ApiResponse<PublicationResponse>(
                    false,
                    "Publication not found",
                    null
                ));

            return Ok(new ApiResponse<PublicationResponse>(
                    true,
                    "Publication retrieved successfully",
                    PublicationMapper.PublicationToPublicationResponse(publication)
                ));

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PublicationResponse>>> CreatePublication([FromBody] PublicationCreatedRequest request)
        {

            Publication publication = PublicationMapper.PublicationCreatedRequestToPublication(request);
            Publication publicationSaved = await _publicationService.SavePublication(publication);

            return StatusCode(StatusCodes.Status201Created, new ApiResponse<PublicationResponse>(
                true,
                "Publication created successfully",
                PublicationMapper.PublicationToPublicationResponse(publicationSaved)
            ));

        }

        [HttpPut("{codePublication}")]
        public async Task<ActionResult<ApiResponse<PublicationResponse>>> EditPublication(long codePublication, [FromBody] PublicationUpdatedRequest request)
        {

            Publication publicationExisting = await _publicationService.GetPublicationByCode(codePublication);

            if (publicationExisting == null)
                return NotFound(new ApiResponse<PublicationResponse>(
                    false,
                    $"Error: Publication whit code {codePublication} does not exist",
                    null
                ));

            Publication publication = PublicationMapper.PublicationUpdatedRequestToPublication(request);

            publication.CodePublication = codePublication;

            Publication publicationUpdate = await _publicationService.UpdatePublication(publication);

            return Ok(new ApiResponse<PublicationResponse>(
                true,
                "Publication updated successfully",
                PublicationMapper.PublicationToPublicationResponse(publicationUpdate)
            ));

        }

        [HttpDelete("{code}")]
        public async Task<ActionResult<ApiResponse<object>>> RemovePublication(long code)
        {
            
            var publicationDeleted = await _publicationService.GetPublicationByCode(code);

            if (publicationDeleted == null)
                return NotFound(new ApiResponse<object>(
                    false,
                    "Publication not found",
                    null
                ));

            await _publicationService.DeletePublication(publicationDeleted.CodePublication);

            return Ok(new ApiResponse<object>(
                true,
                "Publication deleted successfully",
                null
            ));


        }

    }
}
