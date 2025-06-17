using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.data
{
    public interface UserRepository
    {

        public List<User> FindAll();
        public Task<List<User>> FindAllAsync();

        public User? FindById(int id);
        public Task<User?> FindByIdAsync(int id);

    }
}
