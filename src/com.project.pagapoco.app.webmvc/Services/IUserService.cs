using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.app.webmvc.Services
{
    public interface IUserService
    {
        public Task<List<UserResponse>> getUsers();
        public Task<PaginatedResponse<UserResponse>> getUsers(int pageIndex, int pageSize);

    }
}
