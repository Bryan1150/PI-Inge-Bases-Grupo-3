using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class PreguntasFrecuentesModel
    {
        public int idPregunta { get; set; }

        public string categoriaPregunta { get; set; }

        public string topicoPregunta { get; set; }

        public string pregunta { get; set; }

        public string respuesta { get; set; }
    }
}
