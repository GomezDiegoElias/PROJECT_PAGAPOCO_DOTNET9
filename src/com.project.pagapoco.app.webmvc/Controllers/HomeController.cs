using System.Diagnostics;
using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webmvc.Models;
using com.project.pagapoco.app.webmvc.Services.Imp;
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
