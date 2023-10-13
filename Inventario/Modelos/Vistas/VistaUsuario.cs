using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos.Vistas
{
    public class VistaUsuario
    {
        public int IdUsuario { get; set; }
        [Required]
        [MaxLength(30)]
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public string IdTipoProducto { get; set; }
        public string IdCategoria { get; set; }
    }
}
