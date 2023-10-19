using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos.Vistas
{
    public class CreateProducto
    {
        
        [Required]
        [MaxLength(30)]
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NSerie { get; set; }
        public string Caracteristicas { get; set; }
        public int IdTProducto { get; set; }
        public string IdCategoria { get; set; }
    }
}
