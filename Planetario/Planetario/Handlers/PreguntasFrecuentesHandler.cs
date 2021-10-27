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
        private readonly BaseDatosHandler BaseDatos;
        private string Consulta;

        public PreguntasFrecuentesHandler()
        {
            BaseDatos = new BaseDatosHandler();
        }

        public List<PreguntasFrecuentesModel> ObtenerPreguntasFrecuentes()
        {
            List<PreguntasFrecuentesModel> preguntasFrecuentes = new List<PreguntasFrecuentesModel>();
            Consulta = "SELECT * FROM dbo.PreguntasFrecuentes PF JOIN dbo.PreguntasFrecuentesTopicos PFT ON PF.topicoPreguntasFK = PFT.idTopicoPK";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

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
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                categorias.Add(Convert.ToString(columna["categoriaPregunta"]));
            }

            return categorias;
        }

        public bool agregarNuevaPregunta(PreguntasFrecuentesModel nuevaPregunta)
        {     
            bool exito;
            string consulta = "INSERT INTO dbo.PreguntasFrecuentesTopicos (topico1, topico2, topico3) VALUES (@topicoPregunta, @topicoPregunta2, @topicoPregunta3) " +
                "INSERT INTO dbo.PreguntasFrecuentes (categoriaPregunta, pregunta, respuesta, topicoPreguntasFK) VALUES (@categoriaPregunta, @pregunta, @respuesta, scope_identity())";
            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@topicoPregunta",    nuevaPregunta.topicoPregunta },
                { "@categoriaPregunta", nuevaPregunta.categoriaPregunta },
                { "@pregunta",          nuevaPregunta.pregunta },
                { "@respuesta",         nuevaPregunta.respuesta }

            };

            if(nuevaPregunta.topicoPregunta2 != "-Topico-")
            {
                valoresParametros.Add("@topicoPregunta2", nuevaPregunta.topicoPregunta2);
            }
            else
            {
                valoresParametros.Add("@topicoPregunta2", "NULL");
            }

            if (nuevaPregunta.topicoPregunta3 != "-Topico-")
            {
                valoresParametros.Add("@topicoPregunta3", nuevaPregunta.topicoPregunta3);
            }
            else
            {
                valoresParametros.Add("@topicoPregunta3", "NULL");
            }

            exito = BaseDatos.InsertarEnBaseDatos(consulta, valoresParametros);

            return exito;
        }
    }
}

    
