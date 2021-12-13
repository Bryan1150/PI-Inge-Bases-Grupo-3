using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class PagoModel
    {
        public TarjetaModel infoTarjeta { get; set; }
        public ComprableModel comprable { get; set; }
    }
}