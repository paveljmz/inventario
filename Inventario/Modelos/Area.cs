using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos
{
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sirve para poder incrementar de 1 en 1 el id  automaticamente 
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
        public string DireccionArea { get; set; }

        public string Respónsable { get; set; }
       
    }
}
