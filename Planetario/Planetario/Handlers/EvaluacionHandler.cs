using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;


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
            string consulta = "SELECT pregunta FROM PreguntasEvaluacion " +
                "WHERE nombreCuestionarioFK = '" + nombreCuestionario + "';";

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
            string correo = HttpContext.Current.User.Identity.Name;

            string consulta = "INSERT INTO [dbo].[RespuestasEvaluacion] VALUES ";
            List<int> idPreguntas = ObtenerLasPreguntasDelCuestionario(evaluacion.NombreCuestionario);
            if (evaluacion.Respuestas.Count != idPreguntas.Count)
                return false;
            for(int index = 0; index < idPreguntas.Count; ++index )
            {
                int idPregunta = idPreguntas[index];
                string respuesta = evaluacion.Respuestas[index];
                consulta += "(" + idPregunta + ", '" + correo + "', '" + respuesta + "', GETDATE()),";
            }
            consulta = consulta.Remove(consulta.Length - 1);
            exito = InsertarEnBaseDatos(consulta, null);
            return exito;
        }

        public bool InsertarComentario(CuestionarioEvaluacionRecibirModel evaluacion)
        {
            bool exito = false;
            string correo = HttpContext.Current.User.Identity.Name;

            string consulta = "INSERT INTO ComentariosEvaluacion VALUES (@nombreCuestionarioFK, @comentario, @correoPersonaFK, GETDATE())";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@nombreCuestionarioFK", evaluacion.NombreCuestionario },
                {"@correoPersonaFK", correo },
                {"@comentario", evaluacion.Comentario[0] }
            };

            exito = InsertarEnBaseDatos(consulta, valoresParametros);
            return exito;
        }

        public bool InsertarFuncionalidadesEvaluadas(CuestionarioEvaluacionRecibirModel evaluacion)
        {
            bool exito = false;
            string correo = HttpContext.Current.User.Identity.Name;

            string consulta = "INSERT INTO FuncionalidadEvaluada VALUES ";
            string[] funcionalidades = evaluacion.Funcionalidades.Split(';');
            for (int index = 0; index < funcionalidades.Length; ++index)
            {
                string nombreCuestionario = evaluacion.NombreCuestionario;
                string funcionalidad = funcionalidades[index];
                consulta += "('" + nombreCuestionario + "', '" + correo + "', '" + funcionalidad + "', GETDATE()),";
            }
            consulta = consulta.Remove(consulta.Length - 1);
            exito = InsertarEnBaseDatos(consulta, null);
            return exito;
        }

        public List<int> ObtenerLasPreguntasDelCuestionario(string nombreCuestionario)
        {
            string consulta = "SELECT PE.idPreguntaPK FROM PreguntasEvaluacion PE " +
                "JOIN CuestionarioEvaluacion C ON C.nombreCuestionarioPK = PE.nombreCuestionarioFK " +
                "WHERE C.nombreCuestionarioPK = '" + nombreCuestionario + "';";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<int> preguntas = new List<int>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                preguntas.Add(Convert.ToInt32(fila["idPreguntaPK"]));
            }

            return preguntas;
        }
    }
}
