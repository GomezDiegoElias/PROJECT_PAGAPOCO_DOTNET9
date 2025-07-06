using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.app.webmvc.Services.Imp
{
    public interface IUserService
    {
        public Task<PaginatedResponse<UserResponse>> getUsers(int pageIndex, int pageSize);
    }
}
