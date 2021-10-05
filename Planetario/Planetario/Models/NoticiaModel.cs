using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class NoticiaModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el título de la noticia.")]
        [Display(Name = "Ingrese el título de la noticia:")]
        public string titulo { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el cuerpo de la noticia.")]
        [Display(Name = "Ingrese el cuerpo de la noticia:")]
        public string cuerpo { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la fecha de la noticia.")]
        [Display(Name = "Ingrese la fecha de la noticia:")]
        public DateTime fecha { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el correo del autor")]
        [Display(Name = "Ingrese el correo del autor:")]
        public string correoAutor { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la imagen de la noticia.")]
        [Display(Name = "Ingrese la imagen de la noticia:")]
        public HttpPostedFileBase imagen { get; set; }

        public string tipoImagen { get; set; }
    }
}