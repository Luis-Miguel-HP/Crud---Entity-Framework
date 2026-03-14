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


       //contexto de las nuevas tablas
        public DbSet<Categoría> Categoría {  get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Producto> Producto {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                    .Property(p => p.Nombre)
                    .HasMaxLength(50)
                    .IsRequired();
        }
    }
}
