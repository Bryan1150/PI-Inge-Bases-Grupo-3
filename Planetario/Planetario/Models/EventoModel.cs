using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class EventoModel
    {
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Es necesario que ingrese un titulo para el evento")]
        public string Titulo { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Es necesario que ingrese una fecha para el evento")]
        public string Fecha { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Es necesario que ingrese una descripción para el evento")]
        public string Descripcion { get; set; }

        [Display(Name = "Hora")]
        [Required(ErrorMessage = "Es necesario que ingrese una hora para el evento")]
        public string Hora { get; set; }

        [Display(Name = "Link (nombre canal twitch)")]
        [Required(ErrorMessage = "Es necesario que ingrese un link para el evento virtual")]
        public string Link { get; set; }
    }
}