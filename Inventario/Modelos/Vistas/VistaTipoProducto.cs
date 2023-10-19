using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos.Vistas
{
    public class VistaTipoProducto
    {
        [Required]
        public int IdTipoProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreTipoProducto { get; set; }

    }
}
