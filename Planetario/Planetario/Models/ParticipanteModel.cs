using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Planetario.Models
{
    public class ParticipanteModel
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "Es necesario que ingrese su correo electrónico")]
        public string CorreoContacto { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "Es necesario que ingrese su primer apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "Género")]
        [Required(ErrorMessage = "Es necesario que ingrese su género")]
        public string Genero { get; set; }

        [Display(Name = "Ingrese su país")]
        [Required(ErrorMessage = "Es necesario que ingrese su país")]
        public string Pais { get; set; }

        [Display(Name = "Area expertis")]
        [Required(ErrorMessage = "Es necesario que ingrese el área en que es experto")]
        public string AreaExpertis { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Es necesario que ingrese su fecha de nacimiento")]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Nivel Educativo")]
        [Required(ErrorMessage = "Es necesario que ingrese su nivel educativo")]
        public string NivelEducativo { get; set; }
    }
}