using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.core.data.Repository
{
    public class UserRepository : IUserRepository
    {

        // Injección de dependencias del contexto de la base de datos
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> FindAll(int pageIndex, int pageSize)
        {
            return await _dbContext.getUserPagination(pageIndex, pageSize);
        }

        public async Task<User> FindByDni(long dni)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Dni == dni);
        }

        
        public async Task<User> Save(User user)
        {
            await _dbContext.AddAsync(user); // Agrega la entidad al contexto
            await _dbContext.SaveChangesAsync(); // Guarda los cambios en la BD
            return user;
        }

    }
}
