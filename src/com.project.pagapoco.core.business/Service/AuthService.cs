using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.config;
using com.project.pagapoco.core.data.Repository;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Request;
using com.project.pagapoco.core.entities.Dto.Response;
using Microsoft.IdentityModel.Tokens;

namespace com.project.pagapoco.core.business.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtConfig _jwtConfig;

        public AuthService(IUserRepository userRepository, JwtConfig jwtConfig)
        {
            _userRepository = userRepository;
            _jwtConfig = jwtConfig;
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {

            Console.WriteLine($"Intento de login con email: {request.Email}");

            var user = await _userRepository.FindByEmail(request.Email);

            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado");
                throw new UnauthorizedAccessException("Email o contraseña incorrectos");
            }

            Console.WriteLine($"Usuario encontrado: {user.Email}");
            Console.WriteLine($"Contraseña proporcionada: {request.Password}");
            Console.WriteLine($"Hash almacenado: {user.Password}");

            bool passwordValid = VerifyPassword(request.Password, user.Password);
            Console.WriteLine($"Contraseña válida: {passwordValid}");

            if (!passwordValid)
                throw new UnauthorizedAccessException("Email o contraseña incorrectos");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                // Mas claimas
            }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthResponse
            {
                Token = tokenString,
                Expiration = tokenDescriptor.Expires.Value
            };
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {

            if (await _userRepository.FindByEmail(request.Email) != null)
                throw new ApplicationException("Email ya registrado");

            if (await _userRepository.FindByDni(request.Dni) != null)
                throw new ApplicationException("DNI ya registrado");

            var newUser = new User
            {
                Dni = request.Dni,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            var createdUser = await _userRepository.Save(newUser);

            var login  = new LoginRequest
            {
                Email = request.Email,
                Password = request.Password
            };

            return await Login(login);

        }

        public async Task<bool> Logout()
        {
            Console.WriteLine($"Logout realizado en: {DateTime.UtcNow}");
            return await Task.FromResult(true);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

    }
}
