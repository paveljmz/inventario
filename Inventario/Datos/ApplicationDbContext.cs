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
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(new Usuario()
            {
                IdUsuario = 1,
                NombreUsuario = "Pavel",
                CorreoUsuario = "jpavel202@gmail.com",
                TipoUsuario = "administrador",
                PasswordUsuario = "pavel12345",
                IdArea = "tics",
                IdRol = "1",
                FechaCreacion = DateTime.Now

            },
            new Usuario()
            {
                IdUsuario = 2,
                NombreUsuario = "Alejandro",
                CorreoUsuario = "Alex@gmail.com",
                TipoUsuario = "administrador",
                PasswordUsuario = "alex123",
                IdArea = "tics",
                IdRol = "1",
                FechaCreacion = DateTime.Now

            });
            


        }

    }
}
