using System.Collections.Generic;

namespace Planetario.Models
{
    public class CuestionarioEvaluacionRecibirModel : CuestionarioEvalucionModel
    {

        public List<string> Preguntas { get; set; }

        public List<string> Respuestas { get; set; }

        public List<string> Comentario { get; set; }

        public CuestionarioEvaluacionRecibirModel()
        {
            Preguntas = new List<string>();
            Respuestas = new List<string>();
        }
    }
}