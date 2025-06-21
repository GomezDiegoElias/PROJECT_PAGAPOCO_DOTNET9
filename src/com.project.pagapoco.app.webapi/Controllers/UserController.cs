using com.project.pagapoco.app.webapi.Dto.Response;

namespace com.project.pagapoco.app.webapi.Controllers
{
    public interface UserController
    {
        //List<UserResponse> getUsers();
        Task<List<UserResponse>> getUsersAsync(int pageIndex, int pageSize);

    }
}
