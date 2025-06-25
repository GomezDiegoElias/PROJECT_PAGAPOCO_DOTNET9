using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task<User> Update(User user)
        {

            var userUpdate = await this.FindByDni(user.Dni)
                ?? throw new KeyNotFoundException($"User whit DNI {user.Dni} not found");

            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.Email = user.Email;

            if (!string.IsNullOrEmpty(user.Password))
            {
                userUpdate.Password = user.Password;
            }

            await _dbContext.SaveChangesAsync();

            return userUpdate;

        }

        public async Task<bool> DeleteByDni(long dni)
        {

            var user = await this.FindByDni(dni);

            if (user == null) return false;
                
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
