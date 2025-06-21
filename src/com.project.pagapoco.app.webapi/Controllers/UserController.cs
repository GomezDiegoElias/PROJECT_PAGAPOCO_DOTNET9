using com.project.pagapoco.app.webapi.Dto.Response;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{
    public interface UserController
    {

        Task<ActionResult<ApiResponse<List<UserResponse>>>> getAllUsers(int pageIndex, int pageSize);
        Task<ActionResult<ApiResponse<UserResponse?>>> getUser(int id);

    }
}
