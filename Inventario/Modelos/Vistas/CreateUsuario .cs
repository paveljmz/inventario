using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos.Vistas
{
    public class CreateUsuario
    {
      
        [Required]
        [MaxLength(30)]
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }

        public string TipoUsuario { get; set; }
        public string PasswordUsuario { get; set; }
        public string Area { get; set; }
    }
}
