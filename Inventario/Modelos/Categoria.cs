using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sirve para poder incrementar de 1 en 1 el id  automaticamente 
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(50)]  
        public string NCategoria { get; set; }
    }
}
