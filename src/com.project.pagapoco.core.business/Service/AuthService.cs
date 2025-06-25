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

        public async Task<AuthResponse> Authenticate(string email, string password)
        {
            // 1. Validar usuario por email
            var user = await _userRepository.FindByEmailAsync(email);

            if (user == null || !VerifyPassword(password, user.Password))
                throw new UnauthorizedAccessException("Email o contraseña incorrectos");

            // 2. Generar token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                // Agrega más claims según necesites
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

        private bool VerifyPassword(string password, string storedHash)
        {
            // Implementa la verificación de contraseña (usa BCrypt o similar)
            // Esto es solo un ejemplo básico - NO usar en producción
            return password == storedHash;
        }

    }
}
