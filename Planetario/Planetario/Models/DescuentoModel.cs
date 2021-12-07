using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Planetario.Models
{
    public class DescuentoModel
    {
        [Required(ErrorMessage = "Es necesario que indique el codigo de descuento")]
        [Display(Name = "Codigo")]
        [MaxLength(10, ErrorMessage = "Se tiene un máximo de 10 cáracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la cantidad de descuento")]
        [Display(Name = "Descuento")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar números")]
        public int Descuento { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el tipo de membresia")]
        [Display(Name = "Membresia")]
        public string Membresia { get; set; }
    }
}