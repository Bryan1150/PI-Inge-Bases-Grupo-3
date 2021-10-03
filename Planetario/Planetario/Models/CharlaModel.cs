using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Planetario.Models
{
    public class CharlaModel: ActividadModel
    {
        [Required(ErrorMessage = "Es necesario que le indique el contenido que va a tener la charla")]
        [Display(Name = "Ingrese el contenido de la charla")]
        public string nombre { get; set; }
    }
}