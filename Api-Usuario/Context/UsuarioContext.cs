using Api_Usuario.Modelo;
using Microsoft.EntityFrameworkCore;


// este achivo es el que tiene la esencia de entity framework
namespace Api_Usuario.Context
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options)
              : base(options)
        {
          
            
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
