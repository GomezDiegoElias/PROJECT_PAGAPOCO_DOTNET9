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

        //public HomeController(IUserService userService, ILogger<HomeController> logger)
        //{
        //    _userService = userService;
        //    _logger = logger;
        //}

        public HomeController(IPublicationService publicationService, ILogger<HomeController> logger)
        {
            _publicationService = publicationService;
            _logger = logger;
        }

        //public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        //{
        //    page = page < 1 ? 1 : page;
        //    pageSize = pageSize < 1 ? 10 : pageSize;

        //    var users = await _userService.getUsers(page, pageSize);

        //    ViewBag.CurrentPage = page;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalPages = (int)Math.Ceiling(users.TotalCount / (double)pageSize);

        //    return View(users);
        //}

        // Controlador que renderiza la vista principal de publicaciones - Paginación
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {

            // Validación de parámetros de paginación
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // Obtención de publicaciones desde el servicio
            var publications = await _publicationService.GetPublications(pageIndex, pageSize);

            // Manejo de caso sin publicaciones
            ViewBag.CurrentPage = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(publications.TotalCount / (double)pageSize);

            // Renderización de la vista con las publicaciones obtenidas
            return View(publications);

        }

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
