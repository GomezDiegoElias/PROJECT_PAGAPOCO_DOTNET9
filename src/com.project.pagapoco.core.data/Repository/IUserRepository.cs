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

        //public Task<User?> FindById(int id);

        public Task<User> FindByDni(long dni);
        public Task<List<User>> FindAll(int pageIndex, int pageSize);

    }
}
