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
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Display(Name = "País")]
        public string Pais { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Nivel Educativo")]
        public string NivelEducativo { get; set; }

        [Display(Name = "Nombre actividad")]
        public string NombreActividad { get; set; }
    }
}