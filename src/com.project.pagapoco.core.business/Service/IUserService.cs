using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.business.Service
{
    public interface IUserService
    {

        //Task<User?> getUserById(int id);

        Task<User> GetUserByDni(long dni);
        Task<List<User>> GetAllUsers(int pageIndex, int pageSize);

    }
}
