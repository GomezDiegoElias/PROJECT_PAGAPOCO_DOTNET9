using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.business.Service
{
    public interface UserService
    {

        //Task<User?> getUserById(int id);

        Task<User?> getUserByDni(long dni);
        Task<List<User>> getAllUserPagination(int pageIndex, int pageSize);

    }
}
