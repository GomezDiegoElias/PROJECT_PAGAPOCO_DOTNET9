using com.project.pagapoco.core.entities.Dto.Request;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.app.webmvc.Services
{
    public interface IAuthService
    {
        public Task<AuthResponse> Login(LoginRequest request);
        public Task<AuthResponse> Register(RegisterRequest request);
    }
}
