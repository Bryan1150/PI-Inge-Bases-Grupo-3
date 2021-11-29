using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class ComprableModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es necesario que se ingrese un nombre para el producto")]
        public string Nombre { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Es necesario que ingrese un precio")]
        public double Precio { get; set; }

        public int CantidadDisponible { get; set; }
    }
}