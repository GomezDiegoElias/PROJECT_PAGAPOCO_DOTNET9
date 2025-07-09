using com.project.pagapoco.core.entities.Dto.Request;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.app.webmvc.Services.Imp
{
    public interface IAuthService
    {
        public Task<AuthResponse> Login(LoginRequest request);
        public Task<AuthResponse> Register(RegisterRequest request);
        public Task<bool> ResetPassword(string token, string newPassword);
        public Task<bool> SendPasswordResetEmail(string email);
    }
}
