using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.data.Repository
{
    public interface IUserRepository
    {

        public Task<List<User>> FindAll(int pageIndex, int pageSize);
        public Task<User> FindByDni(long dni);
        public Task<User> Save(User user);
        public Task<User> Update(User user);
        public Task<bool> DeleteByDni(long dni);
        public Task<User> FindByEmail(string email);

    }
}
