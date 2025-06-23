using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.app.webapi.Mapper
{
    // Al hacerlo estatico no hace falta hacer la inyeccion de dependencia
    public static class UserMapper
    {

        public static User UserRequestToUser(UserRequest userRequest)
        {
            return new User(
                userRequest.Dni,
                userRequest.FirstName,
                userRequest.LastName,
                userRequest.Email,
                userRequest.Password
                );
        }

        public static UserResponse UserToUserResponse(User user)
        {
            return new UserResponse(
                    user.Id,
                    user.Dni,
                    user.FirstName,
                    user.LastName ?? string.Empty,
                    user.Email
                );
        }

    }
}
