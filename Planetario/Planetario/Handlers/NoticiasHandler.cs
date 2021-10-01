using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Planetario.Handlers
{
    public class NoticiasHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;
        
        public NoticiasHandler()
        {
            rutaConexion = ConfigurationManager.ConnectionStrings["ConexionBaseDatosServidor"].ToString();
            conexion = new SqlConnection(rutaConexion);
        }

        private DataTable crearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();
            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }

        public List<NoticiaModel> obtenerTodasLasNoticias()
        {
            List<NoticiaModel> noticias = new List<NoticiaModel>();
            string consulta = "SELECT * FROM Noticia ";
            DataTable tablaResultado = crearTablaConsulta(consulta);

            foreach(DataRow columna in tablaResultado.Rows)
            {
                noticias.Add(
                    new NoticiaModel
                    {
                        id = Convert.ToInt32(columna["idNoticiaPK"]),
                        titulo = Convert.ToString(columna["titulo"]),
                        cuerpo = Convert.ToString(columna["cuerpo"]),
                        fecha = Convert.ToDateTime(columna["fecha"]),
                        correoAutor = Convert.ToString(columna["correoFuncionarioAutorFK"]),
                    });
            }
            return noticias;
        }

        public NoticiaModel buscarNoticia(string stringId)
        {
            string consulta = "SELECT * FROM Noticia WHERE idNoticiaPK = " + stringId + ";";
            DataTable tablaResultado = crearTablaConsulta(consulta);
            NoticiaModel resultado = null;
            if(tablaResultado.Rows[0] != null)
            {
                resultado = new NoticiaModel
                {
                    id = Convert.ToInt32(tablaResultado.Rows[0]["idNoticiaPK"]),
                    titulo = Convert.ToString(tablaResultado.Rows[0]["titulo"]),
                    cuerpo = Convert.ToString(tablaResultado.Rows[0]["cuerpo"]),
                    fecha = Convert.ToDateTime(tablaResultado.Rows[0]["fecha"]),
                    correoAutor = Convert.ToString(tablaResultado.Rows[0]["correoFuncionarioAutorFK"]),
                };
            }
            return resultado;
        }
    }
}