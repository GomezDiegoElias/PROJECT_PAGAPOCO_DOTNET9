using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.core.data
{
    public class UserRepositoryImp : UserRepository
    {

        // Injección de dependencias del contexto de la base de datos
        private readonly AppDbContext _dbContext;

        public UserRepositoryImp(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para obtener todos los usuarios

        // forma 1
        public List<User> FindAll()
        {
            return _dbContext.Users.ToList();
        }

        // forma 2
        public async Task<List<User>> FindAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        // Método para obtener un usuario por su ID

        // forma 1
        public User? FindById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        // forma 2
        public async Task<User?> FindByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

    }
}
