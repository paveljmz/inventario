using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos
{
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sirve para poder incrementar de 1 en 1 el id  automaticamente 
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
       
        public string TipoUsuario { get; set; }
        public string PasswordUsuario { get; set; }
        public string IdArea { get; set; }
        public string IdRol { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
