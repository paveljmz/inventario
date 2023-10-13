using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos
{
    public class Productos
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sirve para poder incrementar de 1 en 1 el id  automaticamente 
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NSerie { get; set; }
        public string Caracteristicas { get; set; }
        public string IdTipoProducto { get; set; }
        public string IdCategoria { get; set; }
        public DateTime FechaCreacion { get; set; }
    

    }
}
