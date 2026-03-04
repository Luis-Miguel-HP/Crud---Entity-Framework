using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Usuario.Servicios
{
    public class TokenServices
    {
        private readonly string _token;

        public TokenServices()
        {
        }

        public TokenServices(String SecretToken)
        {
            _token = SecretToken;
        }

        public string GeneradorToken(String nombre, string id)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nombre),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddMinutes(10), // El token expira en 24 horas
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Serializar el token a una cadena JWT
            return tokenHandler.WriteToken(token);
        }
    }
}
