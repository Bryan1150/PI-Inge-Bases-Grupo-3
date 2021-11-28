﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class TarjetaModel
    {
        [Required(ErrorMessage = "Es necesario que ingrese un número")]
        [MaxLength(16, ErrorMessage = "Debe ingresar 16 dígitos")]
        [MinLength(16, ErrorMessage = "Debe ingresar 16 dígitos")]
        public string NumeroTarjeta { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese un nombre")]
        public string NombreTarjeta { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese una fecha")]
        [MaxLength(6, ErrorMessage = "Máximo de 4 cáracteres")]
        [MinLength(4, ErrorMessage = "Minimo de 4 cáracteres")]
        public string FechaExpiracion { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese un CVV")]
        [MaxLength(4, ErrorMessage = "Máximo de 4 cáracteres")]
        [MinLength(3, ErrorMessage = "Minimo de 4 cáracteres")]
        public string CVV { get; set; }

        [Display(Name = "Provincia o estado")]
        [Required(ErrorMessage = "Es necesario que ingrese una provincia")]
        public string provincia { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "Es necesario que ingrese una ciudad")]
        public string ciudad { get; set; }

        [Display(Name = "Detalles de dirección")]
        [Required(ErrorMessage = "Es necesario que ingrese detalles de direccion")]
        public string detallesDireccion { get; set; }

        [Display(Name = "Código Postal")]
        [Required(ErrorMessage = "Es necesario que ingrese un codigo postal")]
        public string codigoPostal { get; set; }
    }
}