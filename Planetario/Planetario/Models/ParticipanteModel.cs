using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class ParticipanteModel
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "Es necesario que ingrese su correo electrónico")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string Correo { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es necesario que ingrese un nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "Es necesario que ingrese un apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "Es necesario que ingrese un género")]
        public string Genero { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "Es necesario que ingrese un país")]
        public string Pais { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Es necesario que ingrese una fecha de nacimiento")]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Nivel Educativo")]
        [Required(ErrorMessage = "Es necesario que ingrese un nivel educativo")]
        public string NivelEducativo { get; set; }

        [Display(Name = "Nombre actividad")]
        public string NombreActividad { get; set; }
    }
}