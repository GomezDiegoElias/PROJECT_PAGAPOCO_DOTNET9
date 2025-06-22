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
                userResponse.FirstName, 
                userResponse.LastName,
                userResponse.Email,
                ""
                );
        }

        public static UserResponse UserToUserResponse(User user)
        {
            return new UserResponse(
                    user.Id,
                    user.Dni,
                    user.FirstName,
                    user.LastName,
                    user.Email
                );
        }

    }
}
