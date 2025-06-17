using com.project.pagapoco.core.business;
using com.project.pagapoco.core.entities;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : Controller
    {

        private readonly UserService _userService;

        public UserController(UserService userController)
        {
            _userService = userController;
        }

        [HttpGet("list")]
        public List<User> getAllUsers()
        {
            return _userService.findAll();
        }

        [HttpGet]
        public async Task<List<User>> getAllUsersAsync()
        {
            return await _userService.findAllAsync();
        }

    }
}
