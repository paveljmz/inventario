﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Modelos.Vistas
{
    public class VistaProductos
    {

        public int IdProducto { get; set; }
        [Required]
        [MaxLength(30)]
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NSerie { get; set; }
        public string Caracteristicas { get; set; }
        public string IdTipoProducto { get; set; }
        public string IdCategoria { get; set; }
      

    }
}
