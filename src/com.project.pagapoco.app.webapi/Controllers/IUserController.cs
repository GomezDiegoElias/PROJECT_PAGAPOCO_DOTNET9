using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{
    public interface IUserController
    {

        Task<ActionResult<ApiResponse<List<UserResponse>>>> ListAllUsers(int pageIndex, int pageSize);
        Task<ActionResult<ApiResponse<UserResponse>>> SearchUser(long dni);
        Task<ActionResult<ApiResponse<UserResponse>>> CreateUser(UserCreatedRequest request);
        Task<ActionResult<ApiResponse<UserResponse>>> EditUser(long dni, UserUpdatedRequest request);

    }
}
