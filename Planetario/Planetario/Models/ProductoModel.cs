using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Planetario.Models
{
    public class ProductoModel : ComprableModel
    {
        public int CantidadRebastecer { get; set; }

        public string Tamano { get; set; }

        public string Categoria { get; set; }

        public string Descripcion { get; set; }

        public string FechaIngreso { get; set; }

        public string FechaUltimaVenta { get; set; }

        [Display(Name = "Foto del Producto")]
        public HttpPostedFileBase FotoArchivo { get; set; }

        public string TipoArchivoFoto { get; set; }
    }
}