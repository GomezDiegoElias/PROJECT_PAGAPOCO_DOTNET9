using System.Security.Claims;
using com.project.pagapoco.core.business.Service;
using com.project.pagapoco.core.business.Service.Imp;
using com.project.pagapoco.core.config;
using com.project.pagapoco.core.data.Repository.Imp;
using com.project.pagapoco.core.entities.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.project.pagapoco.app.webapi.Controllers
{

    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/qr")]
    public class QrLoginController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IQrLoginService _qrLoginService;
        private readonly IAuthService _authService;

        public QrLoginController(IUserRepository userRepository, IQrLoginService qrLoginService, IAuthService authService)
        {
            _userRepository = userRepository;
            _qrLoginService = qrLoginService;
            _authService = authService;
        }

        // metodo oficial de QR
        //[Authorize]
        //[HttpGet("generate")]
        //public async Task<IActionResult> GenerateQr()
        //{
        //    var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        //    var user = await _userRepository.FindByEmail(User.FindFirst(ClaimTypes.Email)!.Value);
        //    if (user == null) return Unauthorized();

        //    user.QrSessionId = Guid.NewGuid(); // regenerar QR
        //    await _userRepository.Update(user);

        //    var qrUrl = _qrLoginService.GenerateQrUrl(user.QrSessionId, Request);
        //    var qrImage = _qrLoginService.GenerateQrImage(qrUrl);
        //    return File(qrImage, "image/png");
        //}

        [HttpGet("generate-qr")]
        [Authorize] // debe estar autenticado
        public async Task<IActionResult> GenerateQr()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            // Podés usar el QRSessionId o regenerarlo si querés que expire
            var qrSessionId = Guid.NewGuid(); // o usar uno existente

            // Acá podrías guardar el nuevo qrSessionId en la base
            var user = await _userRepository.FindByEmail(User.FindFirst(ClaimTypes.Email)!.Value);
            if (user == null) return Unauthorized();

            user.QrSessionId = qrSessionId;
            await _userRepository.Update(user);

            var loginUrl = $"http://localhost:5208/api/auth/login-qr/{qrSessionId}";

            // Podés usar una librería de QR como QRCoder o generar un PNG base64
            // Por ahora devolvemos el enlace
            return Ok(new { qrSessionId, loginUrl });
        }


        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> LoginWithQr([FromQuery] Guid sessionId)
        {
            var user = await _userRepository.FindByQrSessionId(sessionId);
            if (user == null) return Unauthorized("QR inválido o expirado");

            user.QrSessionId = Guid.NewGuid(); // invalidar QR
            await _userRepository.Update(user);

            var response = await _authService.Login(new LoginRequest
            {
                Email = user.Email,
                Password = "" // no necesario si generás token directo
            });

            return Ok(response);
        }

    }

}
