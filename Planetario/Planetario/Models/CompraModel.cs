using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class CompraModel
    {
        public ClienteModel participante { get; set; }
        public TarjetaModel tarjeta { get; set; }
        public List<string> productos { get; set; }
        public FacturaModel factura { get; set; }
    }
}