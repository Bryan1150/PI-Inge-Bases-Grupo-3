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
            rutaConexion = ConfigurationManager.ConnectionStrings["ConexionBaseDatosServidor"].ToString();
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
            string consulta = "SELECT * FROM dbo.PreguntasFrecuentes PF JOIN dbo.PreguntasFrecuentesTopicos PFT ON PF.topicoPreguntasFK = PFT.idTopicoPK";
            DataTable tablaResultado = CrearTablaConsulta(consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                preguntasFrecuentes.Add(
                    new PreguntasFrecuentesModel
                    {
                        idPregunta = Convert.ToInt32(columna["idPreguntaPK"]),
                        idTopicos = Convert.ToInt32(columna["topicoPreguntasFK"]),
                        categoriaPregunta = Convert.ToString(columna["categoriaPregunta"]),
                        pregunta = Convert.ToString(columna["pregunta"]),
                        respuesta = Convert.ToString(columna["respuesta"]),
                        topicoPregunta = Convert.ToString(columna["topico1"]),
                        topicoPregunta2 = Convert.ToString(columna["topico2"]),
                        topicoPregunta3 = Convert.ToString(columna["topico3"]),

                    });
            }

            return preguntasFrecuentes;
        }

        public List<String> ObtenerCategorias()
        {
            List<String> categorias = new List<String>();
            string consulta = "SELECT DISTINCT categoriaPregunta FROM dbo.PreguntasFrecuentes";
            DataTable tablaResultado = CrearTablaConsulta(consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                categorias.Add(Convert.ToString(columna["categoriaPregunta"]));
            }

            return categorias;
        }

        public bool agregarNuevaPregunta(PreguntasFrecuentesModel nuevaPregunta)
        {     
            string consulta = "INSERT INTO dbo.PreguntasFrecuentesTopicos (topico1, topico2, topico3) VALUES (@topicoPregunta, @topicoPregunta2, @topicoPregunta3) " +
                "INSERT INTO dbo.PreguntasFrecuentes (categoriaPregunta, pregunta, respuesta, topicoPreguntasFK) VALUES (@categoriaPregunta, @pregunta, @respuesta, scope_identity())";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@topicoPregunta", nuevaPregunta.topicoPregunta);

            if(nuevaPregunta.topicoPregunta2 != "-Topico-")
            {
                comandoParaConsulta.Parameters.AddWithValue("@topicoPregunta2", nuevaPregunta.topicoPregunta2);
            }
            else
            {
                comandoParaConsulta.Parameters.AddWithValue("@topicoPregunta2", "NULL");
            }

            if (nuevaPregunta.topicoPregunta3 != "-Topico-")
            {
                comandoParaConsulta.Parameters.AddWithValue("@topicoPregunta3", nuevaPregunta.topicoPregunta3);
            }
            else
            {
                comandoParaConsulta.Parameters.AddWithValue("@topicoPregunta3", "NULL");
            }

            comandoParaConsulta.Parameters.AddWithValue("@categoriaPregunta", nuevaPregunta.categoriaPregunta);
            comandoParaConsulta.Parameters.AddWithValue("@pregunta", nuevaPregunta.pregunta);
            comandoParaConsulta.Parameters.AddWithValue("@respuesta", nuevaPregunta.respuesta);
          
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }
    }
}

    
