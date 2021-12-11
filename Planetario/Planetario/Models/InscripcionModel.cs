using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planetario.Models;

namespace Planetario.Models
{
    public class InscripcionModel
    {
        public ClienteModel infoParticipante { get; set; }
        public TarjetaModel infoTarjeta { get; set; }
    }
}