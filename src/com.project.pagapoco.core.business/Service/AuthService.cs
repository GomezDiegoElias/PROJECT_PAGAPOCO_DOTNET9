using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.business.Service.Imp;
using com.project.pagapoco.core.config;
using com.project.pagapoco.core.data.Repository.Imp;
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
        private const int SaltLength = 6;
        private const int HashIterations = 10000;
        private readonly EmailService _emailService;

        public AuthService(IUserRepository userRepository, JwtConfig jwtConfig, EmailService emailService)
        {
            _userRepository = userRepository;
            _jwtConfig = jwtConfig;
            _emailService = emailService;
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

            bool passwordValid = VerifyPassword(request.Password, user.Password, user.Salt);
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

            // validaciones
            if (await _userRepository.FindByEmail(request.Email) != null)
                throw new ApplicationException("Email ya registrado");

            if (await _userRepository.FindByDni(request.Dni) != null)
                throw new ApplicationException("DNI ya registrado");

            string salt = GenerateRandomSalt(SaltLength);

            string hashedPassword = HashPasswordWithSalt(request.Password, salt);

            var newUser = new User
            {
                Dni = request.Dni,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hashedPassword,
                Salt = salt
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

        private bool VerifyPassword(string password, string storedHash, string salt)
        {
            // Generar hash con el salt almacenado
            string hashedPassword = HashPasswordWithSalt(password, salt);

            // Comparar los hashes
            return hashedPassword == storedHash;
        }

        private string GenerateRandomSalt(int length)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] salt = new char[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                for (int i = 0; i < length; i++)
                {
                    salt[i] = validChars[randomBytes[i] % validChars.Length];
                }
            }

            return new string(salt);
        }

        // metodo que va en producción
        //private string HashPasswordWithSalt(string password, string salt)
        //{
        //    // Usar PBKDF2 para generar el hash
        //    using (var pbkdf2 = new Rfc2898DeriveBytes(
        //        password: password + salt, // Concatenar password y salt
        //        salt: Encoding.UTF8.GetBytes(salt), // Convertir salt a bytes
        //        iterations: HashIterations,
        //        hashAlgorithm: HashAlgorithmName.SHA256))
        //        // hash generado con SHA256

        //    {
        //        byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes = 256 bits
        //        return Convert.ToHexString(hashBytes).ToLower();
        //    }

        //}

        // Método adicional solo para debug
        // Para corroborar que el hash que se genera es el mismo en caso de que exista contraseñas repetidas
        // El salt es el que varía, pero el hash de la contraseña debería ser el mismo
        private string HashPasswordOnly(string password)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password: password,
                salt: new byte[0], // Sin salt
                iterations: HashIterations,
                hashAlgorithm: HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        private string HashPasswordWithSalt(string password, string salt)
        {
            // 1. Primero hashear solo la contraseña (SOLO PARA DEBUG)
            string passwordOnlyHash = HashPasswordOnly(password);
            Console.WriteLine($"DEBUG - Hash solo de contraseña: {passwordOnlyHash}");

            // 2. Luego hashear con salt (producción)
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password: password + salt,
                salt: Encoding.UTF8.GetBytes(salt),
                iterations: HashIterations,
                hashAlgorithm: HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                string finalHash = Convert.ToHexString(hashBytes).ToLower();
                Console.WriteLine($"DEBUG - Hash con salt: {finalHash}");
                return finalHash;
            }
        }
        public async Task<bool> SendPasswordResetEmail(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            if (user == null) return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _jwtConfig.Issuer, // Añadir issuer
                Audience = _jwtConfig.Audience, // Añadir audience
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var resetToken = tokenHandler.WriteToken(token);

            var resetLink = $"https://localhost:7191/Account/ResetPassword?token={resetToken}";

            await _emailService.SendPasswordResetEmailAsync(email, resetLink);

            return true;
        }
        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtConfig.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // Validar el token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("No se encontró el claim de email en el token");
                    return false;
                }

                var user = await _userRepository.FindByEmail(email);
                if (user == null)
                {
                    Console.WriteLine($"Usuario con email {email} no encontrado");
                    return false;
                }

                // Generar nuevo salt y hash
                var newSalt = GenerateRandomSalt(SaltLength);
                var newHashedPassword = HashPasswordWithSalt(newPassword, newSalt);

                Console.WriteLine($"Actualizando contraseña para usuario: {email}");
                Console.WriteLine($"Nuevo Salt: {newSalt}");
                Console.WriteLine($"Nuevo Hash: {newHashedPassword}");

                user.Password = newHashedPassword;
                user.Salt = newSalt;

                await _userRepository.Update(user);

                Console.WriteLine("Contraseña actualizada exitosamente");
                return true;
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine($"Error de seguridad al validar el token: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al resetear contraseña: {ex}");
                return false;
            }
        }

    }
}
