using com.project.pagapoco.app.webapi.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{
    public interface IUserController
    {

        Task<ActionResult<ApiResponse<List<UserResponse>>>> ListAllUsers(int pageIndex, int pageSize);
        Task<ActionResult<ApiResponse<UserResponse>>> SearchUser(long dni);

    }
}
