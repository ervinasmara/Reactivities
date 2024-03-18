// TokenService.cs

using Domain; // Memanggil class "AppUser"
using Microsoft.IdentityModel.Tokens; // Menggunakan fungsi "SymmetricSecurityKey"
using System.IdentityModel.Tokens.Jwt; // Menggunakan fungsi "JwtSecurityTokenHandler"
using System.Security.Claims; // Menggunakan fungsi "ClaimTypes"
using System.Text; // Menggunakan fungsi "Encoding"

namespace API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string CreateToken(AppUser user)
        {
            // Kita akan membuat daftar klaim yang akan masuk ke dalam dan dikembalikan dengan token kita
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Dan kita perlu menggunakan kunci keamanan simetris
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])); // sesuaikan TokenKey ini dengan appsettings.Development.js tadi
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // ini akan digunakan untuk menandatangani key

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}