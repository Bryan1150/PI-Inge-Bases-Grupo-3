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
        [Display(Name = "Título ")]
        public string titulo { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el cuerpo de la noticia.")]
        [Display(Name = "Cuerpo ")]
        public string cuerpo { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la fecha de la noticia.")]
        [Display(Name = "Fecha ")]
        public string fecha { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el correo del autor")]
        [Display(Name = "Autor ")]
        public string correoAutor { get; set; }

        
        [Display(Name = "Imagen ")]
        public HttpPostedFileBase imagen { get; set; }

        [Display(Name = "Categoría: ")]
        [Required(ErrorMessage = "Es necesario que ingrese la categoría de la noticia")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string categoriaNoticia { get; set; }

        [Display(Name = "Tópico 1: ")]
        [Required(ErrorMessage = "Es necesario que ingrese el tópico de la noticia")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string topicoNoticia { get; set; }

        [Display(Name = "Tópico 2: ")]
        [Required(ErrorMessage = "Es necesario que ingrese el tópico de la noticia")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string topicoNoticia2 { get; set; }

        [Display(Name = "Tópico 3: ")]
        [Required(ErrorMessage = "Es necesario que ingrese el tópico de la noticia")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string topicoNoticia3 { get; set; }

        public string tipoImagen { get; set; }
    }
}