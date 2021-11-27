using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class ComprableModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public int CantidadDisponible { get; set; }
    }
}