using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.app.webapi.Mapper;
using com.project.pagapoco.core.business.Service;
using com.project.pagapoco.core.exceptions;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : Controller, IUserController
    {

        // Inyeccion de dependencia
        private readonly IUserService _userService;

        public UserController(IUserService userController)
        {
            _userService = userController;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserResponse>>>> ListAllUsers(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {

            var users = await _userService.GetAllUsers(pageIndex, pageSize);

            var usersDtos = users
                .Select(user => UserMapper.UserToUserResponse(user))
                .ToList();

            return Ok(new ApiResponse<List<UserResponse>>(
                    "Success",
                    "Users retrieved successfully",
                    usersDtos
                ));

        }

        [HttpGet("{dni}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> SearchUser(long dni)
        {

            var user = await _userService.GetUserByDni(dni);

            if (user == null)
                return NotFound(new ApiResponse<string>(
                        "Error",
                        $"User with DNI '{dni}' does not exist",
                        null
                    ));

            return Ok(new ApiResponse<UserResponse?>(
                    "Success",
                    "Users retrieved successfully",
                    UserMapper.UserToUserResponse(user)
                ));

        }

    }
}
