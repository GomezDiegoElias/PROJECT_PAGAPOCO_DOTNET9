using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.business
{
    public interface UserService
    {

        List<User> findAll();
        Task<List<User>> findAllAsync();
        User? findById(int id);
        Task<User?> findByIdAsync(int id);

        Task<List<User>> getAllUserPagination(int pageIndex, int pageSize);

    }
}
