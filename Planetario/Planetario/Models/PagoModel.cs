using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class PagoModel
    {
        public TarjetaModel infoTarjeta { get; set; }
        public int comprable { get; set; }
        public int cantidadCompra { get; set; }

        [Display(Name = "Nombre completo del recibidor")]
        [Required(ErrorMessage = "Es necesario que se ingrese un nombre.")]
        public string Nombre { get; set; }

        [Display(Name = "Número de teléfono")]
        [Required(ErrorMessage = "Es necesario que se ingrese un número.")]
        public string Telefono { get; set; }

        [Display(Name = "Dirección de envío")]
        [Required(ErrorMessage = "Es necesario que se ingrese una direccion.")]
        public string Direccion { get; set; }
    }
}