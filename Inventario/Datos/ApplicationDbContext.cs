using Inventario.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Datos
{
    public class ApplicationDbContext : DbContext   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //constructor donde se indica que nuestra base "dbcontext" se le va mandar toda la configuracion que tenemos en el servicio atravez de inyeccion de dependencia 
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(new Usuario()
            {
                IdUsuario = 1,
                NombreUsuario = "Pavel",
                CorreoUsuario = "jpavel202@gmail.com",
                TipoUsuario = "administrador",
                PasswordUsuario = "pavel12345",
                Area = "tics",
                FechaCreacion = DateTime.Now

            },
            new Usuario()
            {
                IdUsuario = 2,
                NombreUsuario = "Alejandro",
                CorreoUsuario = "Alex@gmail.com",
                TipoUsuario = "administrador",
                PasswordUsuario = "alex123",
                Area = "tics",
                FechaCreacion = DateTime.Now

            });
        }

    }
}
