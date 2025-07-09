using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.app.webapi.Dto.Response;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.app.webapi.Mapper
{
    // Al hacerlo estatico no hace falta hacer la inyeccion de dependencia
    public static class UserMapper
    {

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

        public static User UserCreatedRequestToUser(UserCreatedRequest request)
        {
            return new User(
                    request.Dni,
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password
                );
        }

        public static User UserUpdatedRequestToUser(UserUpdatedRequest request)
        {
            return new User(
                    request.FirstName,
                    request.LastName,
                    request.Email
                );
        }

        //public static User RegisterRequestToUser(RegisterRequest request)
        //{
        //    return new User(
        //            request.Dni,
        //            request.FirstName,
        //            request.LastName,
        //            request.Email,
        //            request.Password
        //        );
        //}

    }
}
