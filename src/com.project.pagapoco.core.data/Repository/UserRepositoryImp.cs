using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.core.data.Repository
{
    public class UserRepositoryImp : UserRepository
    {

        // Injección de dependencias del contexto de la base de datos
        private readonly AppDbContext _dbContext;

        public UserRepositoryImp(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> FindById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<List<User>> getPaginationUser(int pageIndex, int pageSize)
        {
            return await _dbContext.getUserPagination(pageIndex, pageSize);
        }

    }
}
