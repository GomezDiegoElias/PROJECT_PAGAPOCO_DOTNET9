using System.Security.Claims;
using com.project.pagapoco.app.webmvc.Services;
using com.project.pagapoco.core.entities.Dto.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webmvc.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            try
            {
                var loginRequest = new LoginRequest
                {
                    Email = email,
                    Password = password
                };

                var authResponse = await _authService.Login(loginRequest);

                // Cookie de autenticación
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("JWT", authResponse.Token)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        ExpiresUtc = authResponse.Expiration,
                        IsPersistent = true
                    });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string confirmPassword)
        {
            // Validación
            if (password != confirmPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Por favor complete todos los campos";
            return View();
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

    }
}
