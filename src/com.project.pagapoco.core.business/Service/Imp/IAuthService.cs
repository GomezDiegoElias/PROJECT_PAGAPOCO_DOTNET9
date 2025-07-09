using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Request;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.core.business.Service.Imp
{
    public interface IAuthService
    {
        public Task<AuthResponse> Login(LoginRequest request);
        public Task<AuthResponse> Register(RegisterRequest request);
        public Task<bool> Logout();
        public Task<bool> SendPasswordResetEmail(string email);
        public Task<bool> ResetPassword(string token, string newPassword);
    }
}
