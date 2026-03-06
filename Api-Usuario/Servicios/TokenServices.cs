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

        public string GeneradorToken(string nombre, string id)
        {
            var key = Encoding.UTF8.GetBytes("MiClaveSuperSecretaParaFirmarJWT1234567890");

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, nombre),
            new Claim(ClaimTypes.NameIdentifier, id)
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // metodo para leer el token que ha expirado o que va expirar

        //claims principal es una clase de .net para representar contextos de seguridad, por eso usamos claim,etc

        public ClaimsPrincipal LeerTokenExpirado(string token)
        {
            var ParametrosDeValidacion = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiClaveSuperSecretaParaFirmarJWT1234567890")),
                ValidateLifetime = false
            };
            
            var TokenHandler = new JwtSecurityTokenHandler();

            var Principal = TokenHandler.ValidateToken(
                token,
                ParametrosDeValidacion,
                out SecurityToken securityToken);

                 var jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken == null)
                throw new SecurityTokenException("Token inválido");

            return Principal;

        }
    }
}
