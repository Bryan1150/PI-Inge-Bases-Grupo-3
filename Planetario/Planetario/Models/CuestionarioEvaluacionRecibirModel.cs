using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class CuestionarioEvaluacionRecibirModel : CuestionarioEvalucionModel
    {

        public List<string> Preguntas { get; set; }

        List<string> Respuestas { get; set; }

        List<string> Comentario { get; set; }

        public CuestionarioEvaluacionRecibirModel()
        {
            Preguntas = new List<string>();
            Respuestas = new List<string>();
        }
    }
}