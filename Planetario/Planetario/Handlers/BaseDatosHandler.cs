using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Planetario.Handlers
{
    public class BaseDatosHandler
    {
        private SqlConnection Conexion;
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

        public bool InsertarEnBaseDatos(string consulta, Dictionary<string, object> valoresParametros)
        {
            bool exito;
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, Conexion);

            foreach (KeyValuePair<string, object> parejaValores in valoresParametros)
            {
                comandoParaConsulta.Parameters.AddWithValue(parejaValores.Key, parejaValores.Value);
            }

            Conexion.Open();
            exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            Conexion.Close();

            return exito;
        }

        public System.Tuple<byte[], string> ObtenerArchivo (string consulta, Dictionary<string, object> valoresParametros, string nombreColumnaArchivo, string nombreColumnaTipo)
        {
            byte[] bytes;
            string contentType;
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, Conexion);
            
            foreach (KeyValuePair<string, object> parejaValores in valoresParametros)
            {
                comandoParaConsulta.Parameters.AddWithValue(parejaValores.Key, parejaValores.Value);
            }
            
            Conexion.Open();

            SqlDataReader lectorDeDatos = comandoParaConsulta.ExecuteReader();
            lectorDeDatos.Read();

            bytes = (byte[])lectorDeDatos[nombreColumnaArchivo];
            contentType = lectorDeDatos[nombreColumnaTipo].ToString();

            Conexion.Close();

            return new System.Tuple<byte[], string>(bytes, contentType);
        }

    }
}