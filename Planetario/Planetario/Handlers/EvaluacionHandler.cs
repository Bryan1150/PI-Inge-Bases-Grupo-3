﻿using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace Planetario.Handlers
{
    public class EvaluacionHandler : BaseDatosHandler
    {
        
        public CuestionarioEvaluacionRecibirModel ObtenerCuestionarioRecibir(string nombreCuestionario)
        {
            string consulta = "SELECT * FROM CuestionarioEvaluacion WHERE nombreCuestionarioPK = '" + nombreCuestionario + "';";

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            DataRow columna = tablaResultado.Rows[0];
            CuestionarioEvaluacionRecibirModel evaluacion = new CuestionarioEvaluacionRecibirModel
            {
                NombreCuestionario = nombreCuestionario,
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

        public List<string> ObtenerComentariosDeCuestionario(string nombreCuestionario)
        {
            string consulta = "SELECT comentario FROM ComentariosEvaluacion WHERE nombreCuestionarioFK = '" + nombreCuestionario + "';";
            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> comentarios = new List<string>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                comentarios.Add(Convert.ToString(fila["comentario"]));
            }

            return comentarios;
        }

        public CuestionarioEvaluacionMostrarModel ObtenerCuestionarioMostrar(string nombreCuestionario)
        {
            List<int> preguntas = ObtenerLasPreguntasDelCuestionario(nombreCuestionario);
            List < List<int> > matriz = new List<List<int>>();
            List<int> respuestas = new List<int>();
            List<string> opciones = new List<string> { "Muy en desacuerdo", "En desacuerdo", "Neutro", "De acuerdo", "Muy de acuerdo" };
            foreach (int pregunta in preguntas)
            {
                foreach(string opcion in opciones)
                {
                    respuestas.Add(ObtenerCantidadRespuestas(pregunta, opcion));
                }
                matriz.Add(respuestas);
                respuestas.Clear();
            }

            CuestionarioEvaluacionMostrarModel modelo =
                new CuestionarioEvaluacionMostrarModel {
                    MatrizRespuestas = matriz,
                    Comentario = ObtenerComentariosDeCuestionario(nombreCuestionario)
                };

            return modelo;
        }

        public int ObtenerCantidadRespuestas(int preguntaID, string opcion)
        {
            string consulta = "SELECT COUNT(valorRespuesta) as cantidad FROM PreguntasEvaluacion P " +
                "JOIN RespuestasEvaluacion R ON P.idPreguntaPK = R.idPreguntaFK " +
                "WHERE P.idPreguntaPK = " + preguntaID +
                "AND R.valorRespuesta = '" + opcion + "';";

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            return Convert.ToInt32(tablaResultado.Rows[0]["cantidad"]);
        }

        public List<int> ObtenerCantidadRespuestasPorPregunta(int preguntaID)
        {
            string consulta = "SELECT COUNT(valorRespuesta) as 'Cantidad' FROM PreguntasEvaluacion P " +
                "JOIN RespuestasEvaluacion R ON P.idPreguntaPK = R.idPreguntaFK " +
                "WHERE P.idPreguntaPK = " + preguntaID +
                "GROUP BY valorRespuesta " +
                "ORDER BY CASE WHEN valorRespuesta  = 'Muy en desacuerdo' THEN 1 " +
                              "WHEN valorRespuesta  = 'En desacuerdo' THEN 2 " +
                              "WHEN valorRespuesta  = 'Neutro' THEN 3 " +
                              "WHEN valorRespuesta  = 'De acuerdo' THEN 4 " +
                              "WHEN valorRespuesta  = 'Muy de acuerdo' THEN 5 " +
                         "END ASC";

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            List<int> respuestas = new List<int>();

            foreach (DataRow fila in tablaResultado.Rows)
            {
                respuestas.Add(Convert.ToInt32(fila["Cantidad"]));
            }
            return respuestas;
        }


        public List<int> ObtenerCantidadRespuestasPorPreguntaYFecha(int preguntaID, string fechaInicio, string fechaFinal)
        {
            string consulta = "SELECT COUNT(valorRespuesta) as 'Cantidad' FROM PreguntasEvaluacion P " +
                "JOIN RespuestasEvaluacion R ON P.idPreguntaPK = R.idPreguntaFK " +
                "WHERE P.idPreguntaPK = " + preguntaID + " " +
                "AND R.fechaRespuesta >= '" + fechaInicio + "' " +
                "AND R.fechaRespuesta <= DATEADD(day, 1, '" + fechaFinal + "') " +
                "GROUP BY valorRespuesta " +
                "ORDER BY valorRespuesta ";

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            List<int> respuestas = new List<int>();

            foreach (DataRow fila in tablaResultado.Rows)
            {
                respuestas.Add(Convert.ToInt32(fila["Cantidad"]));
            }
            return respuestas;
        }

        public int ObtenerCantidadPersonas(string nombreCuestionario)
        {
            string consulta = "SELECT COUNT(DISTINCT R.correoFK) as cantidad FROM CuestionarioEvaluacion C " +
                "JOIN PreguntasEvaluacion P ON C.nombreCuestionarioPK = P.nombreCuestionarioFK " +
                "JOIN RespuestasEvaluacion R ON P.idPreguntaPK = R.idPreguntaFK " +
                "WHERE C.nombreCuestionarioPK = '" + nombreCuestionario + "';";

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            return Convert.ToInt32(tablaResultado.Rows[0]["cantidad"]);
        }

        public int ObtenerCantidadPersonasPorFecha(string nombreCuestionario, string fechaInicio, string fechaFinal)
        {
            string consulta = "SELECT COUNT(DISTINCT R.correoFK) as cantidad FROM CuestionarioEvaluacion C " +
                "JOIN PreguntasEvaluacion P ON C.nombreCuestionarioPK = P.nombreCuestionarioFK " +
                "JOIN RespuestasEvaluacion R ON P.idPreguntaPK = R.idPreguntaFK " +
                "WHERE C.nombreCuestionarioPK = '" + nombreCuestionario + "'" +
                "AND R.fechaRespuesta >= '" + fechaInicio + "' " +
                "AND R.fechaRespuesta <= DATEADD(day, 1, '" + fechaFinal + "') ";

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            return Convert.ToInt32(tablaResultado.Rows[0]["cantidad"]);
        }

        public int ObtenerCantidadPersonasEnCruceRespuestas(string nombreCuestionario, List<string> preguntas, List<string> opciones, string fechaInicio, string fechaFinal)
        {
            if (preguntas.Count != opciones.Count || preguntas.Count <= 0)
            {
                return -1;
            }
            string consulta = "SELECT COUNT(DISTINCT R.correoFK) as cantidad FROM CuestionarioEvaluacion C " +
                              "JOIN PreguntasEvaluacion P ON C.nombreCuestionarioPK = P.nombreCuestionarioFK " +
                              "JOIN RespuestasEvaluacion R ON P.idPreguntaPK = R.idPreguntaFK " +
                              "WHERE C.nombreCuestionarioPK = '" + nombreCuestionario + "' " +
                              "AND R.fechaRespuesta >= '" + fechaInicio + "' " +
                              "AND R.fechaRespuesta <= DATEADD(day, 1, '" + fechaFinal + "') " +
                              "AND ( (P.pregunta = '" + preguntas[0] + "' AND R.valorRespuesta = '" + opciones[0] + "')";
            for (int index = 0; index < preguntas.Count; ++index)
            {
                consulta += " OR (P.pregunta = '" + preguntas[index] + "' AND R.valorRespuesta = '" + opciones[index] + "')";
            }
            consulta += ");";
            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            int cantidad = Convert.ToInt32(tablaResultado.Rows[0]["cantidad"]);
            
            return cantidad;
        }

        public string ObtenerRelaciones(string nombreCuestionario, List<string> preguntas, List<string> opciones, string fechaInicio, string fechaFin)
        {
            int cantidad = ObtenerCantidadPersonasEnCruceRespuestas(nombreCuestionario, preguntas, opciones, fechaInicio, fechaFin);

            string respuesta = "En total " + cantidad + " personas están " + opciones[0].ToLower() + " que " + preguntas[0].ToLower();
            for (int index = 1; index < preguntas.Count; ++index)
            {
                respuesta += " y están " + opciones[index].ToLower() + " que " + preguntas[index].ToLower();
            }
            respuesta += ".";

            return respuesta;
        }

    }
}
