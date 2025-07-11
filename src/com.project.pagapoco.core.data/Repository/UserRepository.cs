﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.data.Repository.Imp;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;
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

        public async Task<PaginatedResponse<User>> FindAll(int pageIndex, int pageSize)
        {
            return await _dbContext.getUserPagination(pageIndex, pageSize);
        }

        public async Task<User?> FindByDni(long dni)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Dni == dni);
        }


        public async Task<User> Save(User user)
        {
            await _dbContext.AddAsync(user); // Agrega la entidad al contexto
            await _dbContext.SaveChangesAsync(); // Guarda los cambios en la BD
            return user;
        }

        //public async Task<User> Update(User user)
        //{

        //    var userUpdate = await this.FindByDni(user.Dni)
        //        ?? throw new KeyNotFoundException($"User whit DNI {user.Dni} not found");

        //    //userUpdate.FirstName = user.FirstName;
        //    //userUpdate.LastName = user.LastName;
        //    //userUpdate.Email = user.Email;
        //    userUpdate.Password = user.Password;
        //    userUpdate.Salt = user.Salt;

        //    if (!string.IsNullOrEmpty(user.Password))
        //    {
        //        userUpdate.Password = user.Password;
        //    }

        //    await _dbContext.SaveChangesAsync();

        //    return userUpdate;

        //}

        public async Task<User> Update(User user)
        {
            var userUpdate = await this.FindByDni(user.Dni)
                ?? throw new KeyNotFoundException($"User whit DNI {user.Dni} not found");

            // Asegurarse de que solo actualizamos password y salt
            userUpdate.Password = user.Password;
            userUpdate.Salt = user.Salt;

            // Marcar como modificado explícitamente
            _dbContext.Entry(userUpdate).Property(x => x.Password).IsModified = true;
            _dbContext.Entry(userUpdate).Property(x => x.Salt).IsModified = true;

            try
            {
                await _dbContext.SaveChangesAsync();
                return userUpdate;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteByDni(long dni)
        {

            var user = await this.FindByDni(dni);

            if (user == null) return false;
                
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;

        }
        public async Task<User?> FindByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> FindByQrSessionId(Guid sessionId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.QrSessionId == sessionId);
        }

    }
}
