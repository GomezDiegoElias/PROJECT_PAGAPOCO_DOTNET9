// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");

// Genera una clave de 32 bytes (256 bits)
var keyBytes = new byte[32];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(keyBytes);
}
// Convierte a hexadecimal (para guardar en appsettings.json)
var secretKey = Convert.ToHexString(keyBytes);
Console.WriteLine(secretKey); // Ej: "7a3b8c1d5e9f2a4b6c8d0e1f2a3b4c5d6e7f8a9b0c1d2e3f4a5b6c7d8e9f0a1b"