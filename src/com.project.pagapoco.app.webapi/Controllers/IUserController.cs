using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{
    public interface IUserController
    {

        public Task<ActionResult<ApiResponse<List<UserResponse>>>> ListAllUsers(int pageIndex, int pageSize);
        public Task<ActionResult<ApiResponse<UserResponse>>> SearchUser(long dni);
        public Task<ActionResult<ApiResponse<UserResponse>>> CreateUser(UserCreatedRequest request);
        public Task<ActionResult<ApiResponse<UserResponse>>> EditUser(long dni, UserUpdatedRequest request);
        public Task<ActionResult<ApiResponse<object>>> RemoveUser(long dni);

    }
}
