using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class NoticiaModel
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string cuerpo { get; set; }
        public DateTime fecha { get; set; }
        public string correoAutor { get; set; }
        public HttpPostedFileBase imagen { get; set; }
        public string tipoImagen { get; set; }
    }
}