
using Api_Usuario.Context;
using Api_Usuario.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuario.Servicios
{
    public class ServicesAutenticacion : IAutenticacion
    {
        private readonly UsuarioContext _usuarioContext;

        public ServicesAutenticacion(UsuarioContext user)
        {
            _usuarioContext = user;
        }



        public async Task<string> Autenticacion(string userName, string password)
        {
            try
            {
                Console.WriteLine($"Usuario recibido: {userName}");
                Console.WriteLine($"Password recibido: {password}");

                var user = await _usuarioContext
                    .Usuarios
                    .FirstOrDefaultAsync(p => p.Nombre == userName);

                if (user == null)
                {
                    Console.WriteLine("Usuario NO encontrado");
                    return null;
                }

                Console.WriteLine($"Hash en DB: {user.Password}");
                Console.WriteLine($"Longitud hash: {user.Password.Length}");

                bool valido = BCrypt.Net.BCrypt.Verify(password, user.Password);

                Console.WriteLine($"Resultado Verify: {valido}");

                if (!valido)
                {
                    return null;
                }

                var tokenService = new TokenServices();
                string token = tokenService.GeneradorToken(user.Nombre, user.Id.ToString());

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
