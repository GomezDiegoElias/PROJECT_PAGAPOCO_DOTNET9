using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.app.webapi.Mapper;
using com.project.pagapoco.core.business.Service;
using com.project.pagapoco.core.exceptions;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{

    [ApiController]
    //[Route("/api/[controller]")] // endpoint general -> /api/User
    [Route("/api/users")] // endpoint general -> /api/users
    public class UserControllerImp : Controller, UserController
    {

        // UserMapper se definio como estatico para no realizar inyeccion de dependencia

        // Inyeccion de dependencia
        private readonly UserService _userService;
        //private readonly UserMapper _userMapper;

        public UserControllerImp(UserService userController) //, UserMapper userMapper)
        {
            _userService = userController;
            //_userMapper = userMapper;
        }

        // endpoint -> /api/User?pageIndex=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserResponse>>>> getAllUsers(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {

            //List<User> users
            var users = await _userService.getAllUserPagination(pageIndex, pageSize);

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
        public async Task<ActionResult<ApiResponse<UserResponse?>>> getUser(long dni)
        {

            // Pregunta para Joselo
            // Porque es necesario el uso del try - catch en el controlador si ya estoy capturando en el servicio
            // De no capturarlo aqui en el controlador me sale error "Unhandled"

            try
            {
                // User user
                var user = await _userService.getUserByDni(dni);

                return Ok(new ApiResponse<UserResponse?>(
                        "Success",
                        "Users retrieved successfully",
                        UserMapper.UserToUserResponse(user)
                    ));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(
                        "Error",
                        ex.Message,
                        null
                    ));
            }

            /*User user
            var user = await _userService.getUserById(id);
            ApiResponse<UserResponse?> response = new ApiResponse<UserResponse?>(
                    "Success",
                    "Users retrieved successfully",
                    UserMapper.UserToUserResponse(user)
                );

            return response;*/

        }


    }
}
