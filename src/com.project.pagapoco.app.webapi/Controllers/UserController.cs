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

        /*[HttpGet]
        public async Task<List<User>> getAllUsersAsync()
        {
            return await _userService.findAllAsync();
        }*/

        [HttpGet] // -> /api/User?pageIndex=1&pageSize=10
        public async Task<List<User>> getAllUserPagination
        (
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {

            var results = await _userService.getAllUserPagination(pageIndex, pageSize);
            return results;

        }

    }
}
