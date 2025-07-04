using com.project.pagapoco.app.webmvc.Models;
using com.project.pagapoco.app.webmvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webmvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                pageSize = pageSize < 1 ? 10 : pageSize;

                var paginatedUsers = await _userService.getUsers(pageIndex, pageSize);

                ViewBag.CurrentPage = pageIndex;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling(paginatedUsers.TotalCount / (double)pageSize);
                ViewBag.TotalCount = paginatedUsers.TotalCount;

                return View(paginatedUsers.Items);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }
    }
}
