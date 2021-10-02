using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class PreguntasFrecuentesModel {
        public int idPregunta { get; set; }

        [Display(Name = "Ingrese la categoria de la pregunta")]
        [Required(ErrorMessage = "Es necesario que ingrese la categoria de la pregunta")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string categoriaPregunta { get; set; }

        [Display(Name = "Ingrese el topico de la pregunta")]
        [Required(ErrorMessage = "Es necesario que ingrese el topico de la pregunta")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string topicoPregunta { get; set; }

        [Display(Name = "Ingrese la pregunta")]
        [Required(ErrorMessage = "Es necesario que ingrese la pregunta")]
        public string pregunta { get; set; }

        [Display(Name = "Ingrese la respuesta")]
        [Required(ErrorMessage = "Es necesario que ingrese la respuesta")]
        public string respuesta { get; set; }
    }
}
