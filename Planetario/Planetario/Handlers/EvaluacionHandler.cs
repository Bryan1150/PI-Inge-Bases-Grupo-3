using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Planetario.Models;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data.SqlTypes;
using System.Web;

namespace Planetario.Handlers
{
    public class EvaluacionHandler : BaseDatosHandler
    {
        
        public CuestionarioEvaluacionRecibirModel ObtenerCuestionario(string nombreCuestionario)
        {
            string consulta = "SELECT * FROM CuestionarioEvaluacion WHERE nombreCuestionarioPK = '" + nombreCuestionario + "';";

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            DataRow columna = tablaResultado.Rows[0];
            CuestionarioEvaluacionRecibirModel evaluacion = new CuestionarioEvaluacionRecibirModel
            {
                NombreCuestionario = Convert.ToString(columna["nombreCuestionarioPK"]),
                Categoria = Convert.ToString(columna["categoria"]),
                Preguntas = ObtenerPreguntasDeCuestionario(nombreCuestionario)
            };
            return evaluacion;
        }
        
        public List<string> ObtenerPreguntasDeCuestionario(string nombreCuestionario)
        {
            string consulta = "SELECT PE.pregunta FROM PreguntasEvaluacion PE " +
                "JOIN Cuestionario C ON C.nombreCuestionarioPK = PE.nombreCuestionarioFK " +
                "WHERE C.nombreCuestionarioPK = '" + nombreCuestionario + "';";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> preguntas = new List<string>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                preguntas.Add(Convert.ToString(fila["pregunta"]));
            }

            return preguntas;
        }

        public List<string> ObtenerComentariosDeCuestionario(string nombreCuestionario)
        {
            string consulta = "SELECT comentario FROM CuestionarioEvaluacion WHERE nombreCuestionarioPK = '" + nombreCuestionario + "';";
            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> comentarios = new List<string>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                comentarios.Add(Convert.ToString(fila["pregunta"]));
            }

            return comentarios;
        }

        public bool InsertarRespuestas(CuestionarioEvaluacionRecibirModel evaluacion)
        {
            bool exito = false;
            string consulta = "INSERT INTO [dbo].[RespuestasEvaluacion] + " +
                                "VALUES (@idPregunta, @correoFK, @valorRespuesta, GETDATE())";
            List<int> idPreguntas = ObtenerIdsPreguntasDeCuestionario(evaluacion.NombreCuestionario);
            if (evaluacion.Respuestas.Count != idPreguntas.Count)
                return false;
            for(int index = 0; index < idPreguntas.Count; ++index )
            {
                Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                    {"@idPregunta", idPreguntas[index] },
                    {"@correoFK",  HttpContext.Current.User.Identity.Name},
                    {"@valorRespuesta",  evaluacion.Respuestas[index]}
                };
                exito = InsertarEnBaseDatos(consulta, valoresParametros);
            }
            return exito;
        }

        public List<int> ObtenerIdsPreguntasDeCuestionario(string nombreCuestionario)
        {
            string consulta = "SELECT PE.idPreguntaFK FROM PreguntasEvaluacion PE " +
                "JOIN Cuestionario C ON C.nombreCuestionarioPK = PE.nombreCuestionarioFK " +
                "WHERE C.nombreCuestionarioPK = '" + nombreCuestionario + "';";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<int> preguntas = new List<int>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                preguntas.Add(Convert.ToInt32(fila["pregunta"]));
            }

            return preguntas;
        }
    }
}