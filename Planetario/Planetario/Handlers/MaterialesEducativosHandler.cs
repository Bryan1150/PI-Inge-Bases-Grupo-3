using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Planetario.Models;

namespace Planetario.Handlers
{
    public class MaterialesEducativosHandler
    {
        private readonly SqlConnection Conexion;
        private readonly string RutaConexion;

        public MaterialesEducativosHandler()
        {
            RutaConexion = ConfigurationManager.ConnectionStrings["ConexionBaseDatosServidor"].ToString();
            Conexion = new SqlConnection(RutaConexion);
        }

        private DataTable CrearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, Conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();

            Conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            Conexion.Close();
            return consultaFormatoTabla;
        }

        public List<MaterialEducativoModel> ObtenerInfoTodosLosMateriales()
        {
            List<MaterialEducativoModel> ListaMateriales = new List<MaterialEducativoModel>();
            string Consulta = "SELECT M.titulo, CONVERT(VARCHAR(20), M.fechaSubida, 1) AS fechaSubida , M.correoResponsableFK, " +
                "M.idioma, M.autor, U.nombre + ' ' + U.apellido1 + ' ' + U.apellido2 AS 'nombreResponsable' " +
                "FROM MaterialEducativo M " +
                "JOIN Usuario U ON M.correoResponsableFK = U.correoPK;";
            DataTable tablaResultado = CrearTablaConsulta(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                ListaMateriales.Add(
                    new MaterialEducativoModel
                    {
                        Titulo = 
                    }
                );
            }

            return ListaMateriales;
        }

    }
}