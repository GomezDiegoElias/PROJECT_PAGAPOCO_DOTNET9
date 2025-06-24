using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.business.Service
{
    public interface IUserService
    {

        public Task<List<User>> GetAllUsers(int pageIndex, int pageSize);
        public Task<User> GetUserByDni(long dni);
        public Task<User> SaveUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<bool> DeleteUser(long dni);

    }
}
