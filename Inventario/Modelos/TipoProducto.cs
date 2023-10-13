using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Modelos
{
    public class TipoProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sirve para poder incrementar de 1 en 1 el id  automaticamente 
        public int IdTipoProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string NTipoProducto { get; set; }

    }
}
