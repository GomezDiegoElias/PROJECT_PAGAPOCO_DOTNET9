using com.project.pagapoco.app.webapi.Dto.Request;
using com.project.pagapoco.core.entities.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers.Imp
{
    public interface IAuthController
    {
        public Task<ActionResult> Login(LoginRequest request);
        public Task<ActionResult> Register(RegisterRequest request);
        public Task<ActionResult> Logout();
    }
}
