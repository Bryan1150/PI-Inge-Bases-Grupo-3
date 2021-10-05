using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "Debe ingresar su nombre")]
        [Display(Name = "Nombre ")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar su primer apellido")]
        [Display(Name = "Primer Apellido ")]
        public string apellidoUno { get; set; }

        [Display(Name = "Segundo Apellido ")]
        public string apellidoDos { get; set; }

        [Required(ErrorMessage = "Debe ingresar su correo electrónico")]
        [Display(Name = "Correo Electrónico ")]
        public string correo { get; set; }

        [Required(ErrorMessage = "Debe ingresar su contraseña")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Contraseña ")]
        public string contrasena { get; set; }

        [Required(ErrorMessage = "Debe ingresar el rol")]
        [Display(Name = "Rol ")]
        public int rolId { get; set; }
    }
}