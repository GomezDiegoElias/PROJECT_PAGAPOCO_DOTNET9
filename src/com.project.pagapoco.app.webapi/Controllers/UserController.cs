using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.app.webapi.Mapper;
using com.project.pagapoco.core.business.Service;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{

    [Authorize]
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
                    //"Success",
                    true,
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
                        //"Error: Something went wrong",
                        false,
                        $"Error: User with DNI '{dni}' does not exist",
                        null
                    ));

            return Ok(new ApiResponse<UserResponse?>(
                    //"Success",
                    true,
                    "Users retrieved successfully",
                    UserMapper.UserToUserResponse(user)
                ));

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserResponse>>> CreateUser([FromBody] UserCreatedRequest request)
        {
            User user = UserMapper.UserCreatedRequestToUser(request);
            User userSaved = await _userService.SaveUser(user);

            return StatusCode(StatusCodes.Status201Created, new ApiResponse<UserResponse>(
                    //"Success",
                    true,
                    "User created successfully",
                    UserMapper.UserToUserResponse(userSaved)
                ));

        }

        [HttpPut("{dni}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> EditUser(long dni, [FromBody] UserUpdatedRequest request)
        {

            User userExisting = await _userService.GetUserByDni(dni);

            if (userExisting == null)
                return NotFound(new ApiResponse<string>(
                        //"Error: Something went wrong",
                        false,
                        $"Error: User with DNI {dni} does not exist",
                        null
                    ));

            User user = UserMapper.UserUpdatedRequestToUser(request);

            user.Dni = userExisting.Dni;

            User userUpdate = await _userService.UpdateUser(user);

            return Ok(new ApiResponse<UserResponse>(
                    //"Success",
                    true,
                    "User updated successfully",
                    UserMapper.UserToUserResponse(userUpdate)
                ));

        }

        [HttpDelete("{dni}")]
        public async Task<ActionResult<ApiResponse<object>>> RemoveUser(long dni)
        {

            User user = await _userService.GetUserByDni(dni);

            if (user == null)
                return NotFound(new ApiResponse<object>(
                        //"Error: Something went wrong",
                        false,
                        $"Error: User with DNI {dni} does not exist",
                        null
                    ));

            await _userService.DeleteUser(dni);

            return Ok(new ApiResponse<object>(
                    //"Success",
                    true,
                    "User deleted successfully",
                    null
                ));

        }

    }
}
