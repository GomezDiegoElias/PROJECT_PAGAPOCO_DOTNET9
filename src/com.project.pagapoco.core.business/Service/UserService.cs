using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.business.Service.Imp;
using com.project.pagapoco.core.data.Repository.Imp;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;
using com.project.pagapoco.core.exceptions;

namespace com.project.pagapoco.core.business.Service
{
    public class UserService : IUserService
    {

        // Inyección de dependencias del repositorio de usuarios
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedResponse<User>> GetAllUsers(int pageIndex, int pageSize)
        {
            return await _userRepository.FindAll(pageIndex, pageSize);
        }

        public async Task<User> GetUserByDni(long dni)
        {
            User user = await _userRepository.FindByDni(dni);
            return user;
        }

        public Task<User> SaveUser(User user)
        {
            return _userRepository.Save(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.Update(user);
        }

        public async Task<bool> DeleteUser(long dni)
        {
            return await _userRepository.DeleteByDni(dni);
        }

    }
}
