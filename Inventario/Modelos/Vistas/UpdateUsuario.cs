using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos.Vistas
{
    public class UpdateUsuario
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        [MaxLength(30)]
        public string NombreUsuario { get; set; }
        [Required]
        public string CorreoUsuario { get; set; }
        [Required]
        public string TipoUsuario { get; set; }
        [Required]
        public string PasswordUsuario { get; set; }
        [Required]
        public string Area { get; set; }
    }
}
