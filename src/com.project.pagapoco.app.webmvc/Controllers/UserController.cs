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

        public async Task<IActionResult> Index()
        {

            try
            {
                var users = await _userService.getUsers();
                return View(users);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                // Log the error
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }

        }
    }
}
