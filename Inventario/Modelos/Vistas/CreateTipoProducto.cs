﻿using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos.Vistas
{
    public class CreateTipoProducto
    {
        [Required]
        public int IdTipoProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreTipoProducto { get; set; }
    }
}
