using System.Diagnostics;
using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.app.webmvc.Models;
using com.project.pagapoco.app.webmvc.Services.Imp;
using com.project.pagapoco.core.entities.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webmvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        //private readonly IUserService _userService;
        private readonly IPublicationService _publicationService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IPublicationService publicationService, ILogger<HomeController> logger)
        {
            _publicationService = publicationService;
            _logger = logger;
        }

        // Funcion de paginación de publicaciones
        // Controlador que renderiza la vista principal de publicaciones - Paginación
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            // Validación de parámetros de paginación
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // Obtención de publicaciones desde el servicio
            var publications = await _publicationService.GetPublications(page, pageSize);

            // Manejo de caso sin publicaciones
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(publications.TotalCount / (double)pageSize);

            // Renderización de la vista con las publicaciones obtenidas
            return View(publications);

        }

        // Hacer aqui las demas funciones del controlador de la entidad Publication

        // ...

        /*
            1. Inicio de sesion y registro de usuarios (TERMINADO)
            2. Funcion de paginación de publicaciones (TERMINADO)
            - (FALTA) 
                • USAR EL MISMO FORMULARIO DE CREACIÓN PARA LA EDICIÓN DE PUBLICACIONES
            4. Función para mostrar el formulario de creación de una nueva publicación
            5. Función para crear una nueva publicación.
            6. Función para mostrar el formulario de edición de una publicación.
            7. Función para editar una publicación existente.
            8. Función para eliminar una publicación.
            9. Función para mostrar los detalles de una publicación específica.
            10. Función para buscar publicaciones por codigo de publicacion.

         */

        [HttpGet]
        public IActionResult FormPublication()
        {
            return View(new PublicationCreatedRequest(
                Code: 0,
                Title: "",
                Description: "",
                Price: 0,
                Brand: "",
                Model: "",
                Year: 0,
                UserId: 0
            ));
        }

        [HttpPost]
        public async Task<IActionResult> FormPublication(PublicationCreatedRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                var publication = await _publicationService.CreatedPublication(request);
                TempData["SuccessMessage"] = "Publicación creada exitosamente";
                return RedirectToAction("Index");
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPublicationDetails(long code)
        {
            try
            {
                var publication = await _publicationService.GetPublicationByCode(code);

                if (publication == null)
                {
                    return NotFound();
                }

                return PartialView("_PublicationDetails", publication);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener detalles de publicación");
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePublication(long code)
        {
            try
            {
                var result = await _publicationService.DeletePublication(code);
                if (result)
                {
                    return Ok();
                }
                return BadRequest("No se pudo eliminar la publicación");
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(long code, int page = 1, int pageSize = 10)
        {
            try
            {
                var publication = await _publicationService.GetPublicationByCode(code);

                if (publication == null)
                {
                    TempData["ErrorMessage"] = "No se encontró la publicación con el código especificado";
                    return RedirectToAction("Index");
                }

                var paginatedResponse = new PaginatedResponse<PublicationResponse>
                {
                    Items = new List<PublicationResponse> { publication },
                    TotalCount = 1
                };

                ViewBag.CurrentPage = 1;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = 1;
                ViewBag.SearchCode = code;

                return View("Index", paginatedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar publicación");
                TempData["ErrorMessage"] = "Ocurrió un error al buscar la publicación";
                return RedirectToAction("Index");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> EditPublication(long code)
        //{
        //    try
        //    {
        //        var publication = await _publicationService.GetPublicationByCode(code);

        //        if (publication == null)
        //        {
        //            return NotFound();
        //        }

        //        var request = new PublicationCreatedRequest(
        //            Code: publication.CodePublication,
        //            Title: publication.Title,
        //            Description: publication.Description,
        //            Price: publication.Price,
        //            Brand: publication.Brand,
        //            Model: publication.Model,
        //            Year: int.Parse(publication.Year),
        //            UserId: publication.UserId
        //        );

        //        return View("FormPublication", request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error al cargar publicación para editar");
        //        TempData["ErrorMessage"] = "Error al cargar la publicación para editar";
        //        return RedirectToAction("Index");
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> EditPublication(long code)
        {
            try
            {
                var publication = await _publicationService.GetPublicationByCode(code);

                if (publication == null)
                {
                    return NotFound();
                }

                var request = new PublicationCreatedRequest(
                    Code: publication.CodePublication,
                    Title: publication.Title,
                    Description: publication.Description,
                    Price: publication.Price,
                    Brand: publication.Brand,
                    Model: publication.Model,
                    Year: int.Parse(publication.Year),
                    UserId: publication.UserId
                );

                return View("EditPublication", request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar publicación para editar");
                TempData["ErrorMessage"] = "Error al cargar la publicación para editar";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePublication(PublicationCreatedRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("FormPublication", request);
            }

            try
            {
                var updatedPublication = await _publicationService.UpdatePublication(request);
                TempData["SuccessMessage"] = "Publicación actualizada exitosamente";
                return RedirectToAction("Index");
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("FormPublication", request);
            }
        }

        // Otras funciones del controlador por defecto
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
