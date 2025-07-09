using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using com.project.pagapoco.app.webmvc.Models;
using com.project.pagapoco.app.webmvc.Services.Imp;
using com.project.pagapoco.core.config;
using com.project.pagapoco.core.entities.Dto.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace com.project.pagapoco.app.webmvc.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly IAuthService _authService;
        private readonly JwtConfig _jwtConfig;

        public AccountController(IAuthService authService, IOptions<JwtConfig> jwtOptions)
        {
            _authService = authService;
            _jwtConfig = jwtOptions.Value;
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
        public async Task<IActionResult> Register(long dni, string firstname, string lastname, string email, string password)
        {
            
            try
            {

                if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
                {
                    ViewBag.Error = "Por favor complete todos los campos";
                    return View();

                }

                var registerRequest = new RegisterRequest
                {
                    Dni = dni,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Password = password
                };

                var authResponse = await _authService.Register(registerRequest);

                return RedirectToAction("Login");

            } catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

        // GET: Account/ResetPassword?token=xxxx
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        // POST: Account/ResetPassword
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    try
        //    {
        //        var handler = new JwtSecurityTokenHandler();
        //        var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

        //        var validationParams = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidIssuer = _jwtConfig.Issuer,
        //            ValidAudience = _jwtConfig.Audience,
        //            ValidateLifetime = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ClockSkew = TimeSpan.Zero
        //        };

        //        var principal = handler.ValidateToken(model.Token, validationParams, out _);
        //        var email = principal.FindFirst(ClaimTypes.Email)?.Value;

        //        if (email == null)
        //        {
        //            ModelState.AddModelError("", "Token inválido.");
        //            return View(model);
        //        }

        //        await _authService.ResetPassword(model.Token, model.NewPassword);

        //        TempData["Message"] = "Contraseña actualizada con éxito.";
        //        return RedirectToAction("Login");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Token inválido o expirado.");
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    Console.WriteLine($"Attempting to reset password with token: {model.Token}");

        //    try
        //    {
        //        var result = await _authService.ResetPassword(model.Token, model.NewPassword);

        //        if (result)
        //        {
        //            Console.WriteLine("Password reset successful");
        //            TempData["Message"] = "Contraseña actualizada con éxito.";
        //            return RedirectToAction("Login");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Password reset failed (service returned false)");
        //            ModelState.AddModelError("", "No se pudo actualizar la contraseña. Intente nuevamente.");
        //            return View(model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in password reset: {ex.Message}");
        //        ModelState.AddModelError("", "Token inválido o expirado.");
        //        return View(model);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Console.WriteLine($"Intentando resetear contraseña con token: {model.Token}");

            try
            {
                var result = await _authService.ResetPassword(model.Token, model.NewPassword);

                if (result)
                {
                    TempData["Message"] = "Contraseña actualizada con éxito.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar la contraseña. El token puede ser inválido o haber expirado.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en reset de contraseña: {ex}");
                ModelState.AddModelError("", "Ocurrió un error al procesar tu solicitud. Por favor intenta nuevamente.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Por favor ingrese un correo electrónico.";
                return View();
            }

            var result = await _authService.SendPasswordResetEmail(email);
            TempData["Message"] = result
                ? "Si el correo existe, se envió un enlace para restablecer la contraseña."
                : "No se pudo enviar el correo. Intente nuevamente.";

            return RedirectToAction("Login");
        }


        // GET: Account/Logout
        public IActionResult Logout()
        {
            // Eliminar la cookie de autenticación
            return RedirectToAction("Login");
        }

    }
}
