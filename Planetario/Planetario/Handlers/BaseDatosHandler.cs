using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Planetario.Handlers
{
    public class BaseDatosHandler
    {
        public SqlConnection Conexion { get; }
        private readonly string RutaConexion;

        public BaseDatosHandler()
        {
            RutaConexion = ConfigurationManager.ConnectionStrings["ConexionBaseDatosServidor"].ToString();
            Conexion = new SqlConnection(RutaConexion);
        }

        public DataTable LeerBaseDeDatos(string consulta)
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