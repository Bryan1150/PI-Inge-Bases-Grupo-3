using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class FacturaModel
    {
        public DateTime FechaCompra { get; set; }

        public decimal PagoTotal { get; set; }

        public string CorreoParticipante { get; set; }

        public string NombreActividad { get; set; }
    }
}