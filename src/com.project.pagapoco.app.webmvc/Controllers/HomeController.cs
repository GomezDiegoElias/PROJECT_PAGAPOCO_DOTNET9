using System.Diagnostics;
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

        // Funcion de paginaci�n de publicaciones
        // Controlador que renderiza la vista principal de publicaciones - Paginaci�n
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            // Validaci�n de par�metros de paginaci�n
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // Obtenci�n de publicaciones desde el servicio
            var publications = await _publicationService.GetPublications(page, pageSize);

            // Manejo de caso sin publicaciones
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(publications.TotalCount / (double)pageSize);

            // Renderizaci�n de la vista con las publicaciones obtenidas
            return View(publications);

        }

        // Hacer aqui las demas funciones del controlador de la entidad Publication

        // ...

        /*
            1. Inicio de sesion y registro de usuarios (TERMINADO)
            2. Funcion de paginaci�n de publicaciones (TERMINADO)
            - (FALTA) 
                � USAR EL MISMO FORMULARIO DE CREACI�N PARA LA EDICI�N DE PUBLICACIONES
            4. Funci�n para mostrar el formulario de creaci�n de una nueva publicaci�n
            5. Funci�n para crear una nueva publicaci�n.
            6. Funci�n para mostrar el formulario de edici�n de una publicaci�n.
            7. Funci�n para editar una publicaci�n existente.
            8. Funci�n para eliminar una publicaci�n.
            9. Funci�n para mostrar los detalles de una publicaci�n espec�fica.
            10. Funci�n para buscar publicaciones por codigo de publicacion.

         */


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
