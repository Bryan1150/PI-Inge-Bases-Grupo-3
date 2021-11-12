using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class FacturaModel
    {
        public int ID {get;set;}

        public string FechaCompra { get; set; }

        public double PagoTotal { get; set; }

        public string CorreoParticipante { get; set; }

        public string NombreActividad { get; set; }
    }
}