using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Planetario.Handlers
{
    public class PreguntasFrecuentesHandler
    {
        private SqlConnection conexion;
        private readonly string rutaConexion;

        public PreguntasFrecuentesHandler()
        {
            rutaConexion = ConfigurationManager.ConnectionStrings["PlanetarioConnection"].ToString();
            conexion = new SqlConnection(rutaConexion);
        }

        private DataTable CrearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();

            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }

        public List<PreguntasFrecuentesModel> ObtenerPreguntasFrecuentes()
        {
            List<PreguntasFrecuentesModel> preguntasFrecuentes = new List<PreguntasFrecuentesModel>();
            string consulta = "SELECT * FROM dbo.PreguntasFrecuentes";
            DataTable tablaResultado = CrearTablaConsulta(consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                preguntasFrecuentes.Add(
                    new PreguntasFrecuentesModel
                    {
                        idPregunta = Convert.ToInt32(columna["idPreguntaPK"]),
                        categoriaPregunta = Convert.ToString(columna["categoriaPregunta"]),
                        topicoPregunta = Convert.ToString(columna["topicoPregunta"]),
                        pregunta = Convert.ToString(columna["pregunta"]),
                        respuesta = Convert.ToString(columna["respuesta"]),
                    });
            }

            return preguntasFrecuentes;
        }

        public bool agregarNuevaPregunta(PreguntasFrecuentesModel nuevaPregunta)
        {
            string consulta = "INSERT INTO dbo.PreguntasFrecuentes VALUES (categoriaPregunta, topicoPregunta, pregunta, respuesta) " +
                "VALUES (@categoriaPregunta, @topicoPregunta, @pregunta, @respuesta) ";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@categoriaPregunta", nuevaPregunta.categoriaPregunta);
            comandoParaConsulta.Parameters.AddWithValue("@topicoPregunta", nuevaPregunta.topicoPregunta);
            comandoParaConsulta.Parameters.AddWithValue("@pregunta", nuevaPregunta.pregunta);
            comandoParaConsulta.Parameters.AddWithValue("@respuesta", nuevaPregunta.respuesta);
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }
    }
}

    
