﻿using System;
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

        public int CantidadCarrito { get; set; }

        public string Tamano { get; set; }

        public string Categoria { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "Fecha de ingreso")]
        [Required(ErrorMessage = "Es necesario que ingrese una fecha de ingreso")]
        public string FechaIngreso { get; set; }

        public string FechaUltimaVenta { get; set; }

        [Display(Name = "Foto del Producto")]
        public HttpPostedFileBase FotoArchivo { get; set; }

        public string TipoArchivoFoto { get; set; }
    }
}