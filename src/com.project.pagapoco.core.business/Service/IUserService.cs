using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.business.Service
{
    public interface IUserService
    {

        Task<List<User>> GetAllUsers(int pageIndex, int pageSize);
        Task<User> GetUserByDni(long dni);
        Task<User> SaveUser(User user);

    }
}
