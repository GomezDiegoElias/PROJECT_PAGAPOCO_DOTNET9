using System.Diagnostics;
using com.project.pagapoco.app.webmvc.Models;
using com.project.pagapoco.app.webmvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webmvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUserService userService, ILogger<HomeController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        //public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        //{
        //    try
        //    {

        //        pageIndex = pageIndex < 1 ? 1 : pageIndex;
        //        pageSize = pageSize < 1 ? 10 : pageSize;

        //        var users = await _userService.getUsers(pageIndex, pageSize);

        //        ViewBag.CurrentPage = pageIndex;
        //        ViewBag.PageSize = pageSize;
        //        ViewBag.TotalPages = (int)Math.Ceiling(users.TotalCount / (double)pageSize);

        //        return View(users);

        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error
        //        return View("Error", new ErrorViewModel { Message = ex.Message });
        //    }
        //}

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var users = await _userService.getUsers(page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(users.TotalCount / (double)pageSize);

            return View(users);
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
