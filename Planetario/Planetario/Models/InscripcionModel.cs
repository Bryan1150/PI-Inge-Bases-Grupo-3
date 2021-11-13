using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planetario.Models;

namespace Planetario.Models
{
    public class InscripcionModel
    {
        public ParticipanteModel infoParticipante { get; set; }
        public FacturaModel infoFactura { get; set; }
    }
}