using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Planetario.Handlers
{
    public class FuncionariosHandler
    {
        private readonly SqlConnection Conexion;
        private readonly string RutaConexion;

        public FuncionariosHandler()
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

    }
}