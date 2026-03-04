
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



        public async Task<string> Autenticacion(string userName, string password) {
                
        
            try
            {   
                var user= await _usuarioContext.Usuarios.FirstOrDefaultAsync(p => p.Nombre == userName);

                if (user?.Nombre == null) {
                    return null;
                }
                else
                {
                    bool valido = BCrypt.Net.BCrypt.Verify(user.Password, password);
                    if (!valido) {
                        return null;
                    }
                    else
                    {
                        Console.WriteLine();
                        var token = new TokenServices();
                        string Token =token.GeneradorToken(user.Nombre,  user.Id.ToString());
                        return Token;
                    }
                }
                    
            }
            catch (Exception ex) { 
                return ex.Message;
            }
            
        }
    }
}
