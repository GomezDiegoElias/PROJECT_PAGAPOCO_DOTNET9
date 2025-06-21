using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.data;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.business
{
    public class UserServiceImp : UserService
    {

        // Inyección de dependencias del repositorio de usuarios
        private readonly UserRepository _userRepository;

        public UserServiceImp(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> findAll()
        {
            
            List<User> users = _userRepository.FindAll();

            if (users == null || users.Count == 0)
            {
                throw new Exception("Lista vacia");
            }

            return users;

        }

        /*public Task<List<User>> findAllAsync()
        {

            List<User> users = _userRepository.FindAllAsync().Result;

            if (users == null || users.Count == 0)
            {
                throw new Exception("Lista vacia");
            }

            return Task.FromResult(users);

        }*/

        public async Task<List<User>> findAllAsync()
        {

            List<User> users = await _userRepository.FindAllAsync();

            if (users == null || users.Count == 0)
            {
                throw new Exception("Lista vacia");
            }

            return users;

        }

        public User? findById(int id)
        {

            User? user = _userRepository.FindById(id);

            if (user == null)
            {
                throw new Exception("Usuario no existe");
            }

            return user;

        }

        public async Task<User?> findByIdAsync(int id)
        {

            User? user = await _userRepository.FindByIdAsync(id);

            if(user == null)
            {
                throw new Exception("Usuario no existe");
            }

            return user;

        }

        public async Task<List<User>> getAllUserPagination(int pageIndex, int pageSize)
        {
            return await _userRepository.getPaginationUser(pageIndex, pageSize);
        }
    }
}
