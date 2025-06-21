using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.data.Repository;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.exceptions;

namespace com.project.pagapoco.core.business.Service
{
    public class UserServiceImp : UserService
    {

        // Inyección de dependencias del repositorio de usuarios
        private readonly UserRepository _userRepository;

        public UserServiceImp(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> getAllUserPagination(int pageIndex, int pageSize)
        {
            return await _userRepository.getPaginationUser(pageIndex, pageSize);
        }

        public async Task<User?> getUserById(int id)
        {

            User? user = await _userRepository.FindById(id);

            if (user == null)
            {
                throw new NotFoundException($"User with ID '{id}' does not exist");
            }

            return user;

        }

    }
}
