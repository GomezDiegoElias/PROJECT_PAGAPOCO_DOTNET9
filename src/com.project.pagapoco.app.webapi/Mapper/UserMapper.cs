using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.app.webapi.Mapper
{
    public static class UserMapper
    {

        public static User UserResponseToUser(UserResponse userResponse)
        {
            return new User(
                userResponse.Id, 
                userResponse.Nombre, 
                userResponse.Apellido,
                userResponse.Correo,
                ""
                );
        }

        public static UserResponse UserToUserResponse(User user)
        {
            return new UserResponse(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email
                );
        }

    }
}
