using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.core.business.Service
{
    public interface IUserService
    {

        public Task<PaginatedResponse<User>> GetAllUsers(int pageIndex, int pageSize);
        public Task<User> GetUserByDni(long dni);
        public Task<User> SaveUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<bool> DeleteUser(long dni);

    }
}
