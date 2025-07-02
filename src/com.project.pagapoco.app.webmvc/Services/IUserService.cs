using com.project.pagapoco.app.webapi.Dto.Response;

namespace com.project.pagapoco.app.webmvc.Services
{
    public interface IUserService
    {
        public Task<List<UserResponse>> getUsers();
        public Task<List<UserResponse>> getUsers(int pageIndex, int pageSize);

    }
}
