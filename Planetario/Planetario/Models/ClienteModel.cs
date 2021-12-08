using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class ClienteModel: PersonaModel
    {
        [Display(Name = "Nivel Educativo")]
        [Required(ErrorMessage = "Es necesario que ingrese un nivel educativo")]
        public string nivelEducativo { get; set; }

        public string membresia { get; set; }
    }
}