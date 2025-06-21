using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.app.webapi.Mapper;
using com.project.pagapoco.core.business;
using com.project.pagapoco.core.entities;
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
        public async Task<List<UserResponse>> getUsersAsync(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {
            List<User> users = await _userService.getAllUserPagination(pageIndex, pageSize);
            return users
                .Select(user => UserMapper.UserToUserResponse(user))
                .ToList();
        }

        // endpoint -> /api/User/list
        /*[HttpGet("list")]
        public List<UserResponse> getUsers()
        {
            List<User> users = _userService.findAll();
            return users
                .Select(user => UserMapper.UserToUserResponse(user))
                .ToList();
        }*/

        // ░░░░░░░░░░░░░░░░░░░░░░░░░░ PRUEBAS ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░

        // Prueba 1
        /*[HttpGet("list")]
        public List<User> getAllUsers()
        {
            return _userService.findAll();
        }*/

        // Prueba 2 con mapeo
        /*[HttpGet("list")]
        public List<UserResponse> GetUser()
        {
            List<User> users = _userService.findAll();
            List<UserResponse> response = users
                .Select(user => UserMapper.UserToUserResponse(user)).ToList();
            return response;
        }*/

        // Prueba 1
        /*[HttpGet]
        public async Task<List<User>> getAllUsersAsync()
        {
            return await _userService.findAllAsync();
        }*/

        /*[HttpGet] // -> /api/User?pageIndex=1&pageSize=10
        public async Task<List<User>> getAllUserPagination
        (
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {

            List<User> results = await _userService.getAllUserPagination(pageIndex, pageSize);
            return results;

        }*/

        // Prueba 2 con mapeo
        /*[HttpGet] // -> /api/User?pageIndex=1&pageSize=10
        public async Task<List<UserResponse>> getAllUserPagination
        (
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {

            List<User> users = await _userService.getAllUserPagination(pageIndex, pageSize);

            List<UserResponse> response = users
                .Select(user => UserMapper.UserToUserResponse(user))
                .ToList();

            return response;

        }*/

    }
}
