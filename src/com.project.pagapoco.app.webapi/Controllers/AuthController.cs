using com.project.pagapoco.core.business.Service;
using com.project.pagapoco.core.entities.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{

    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller, IAuthController
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {

            try
            {
                var response = await _authService.Login(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {

            try
            {
                var response = await _authService.Register(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al registrar", error = ex.Message });

            }
        }

        public async Task<ActionResult> Logout()
        {
            try
            {
                var result = await _authService.Logout();

                if (result)
                {
                    return Ok(new
                    {
                        message = "Logout exitoso",
                        timestamp = DateTime.UtcNow
                    });
                }
                else
                {
                    return BadRequest(new { message = "Error durante el logout" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error durante el logout", error = ex.Message });
            }
        }

    }
}
