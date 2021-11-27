using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System;

namespace Planetario.Handlers
{
    public class BaseDatosHandler
    {
        private SqlConnection conexion;
        private readonly string rutaConexion;

        public BaseDatosHandler()
        {
            rutaConexion = ConfigurationManager.ConnectionStrings["ConexionBaseDatosServidor"].ToString();
            conexion = new SqlConnection(rutaConexion);
        }

        public DataTable LeerBaseDeDatos(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();

            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }

        public bool InsertarEnBaseDatos(string consulta, Dictionary<string, object> valoresParametros)
        {
            bool exito;
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);

            foreach (KeyValuePair<string, object> parejaValores in valoresParametros)
            {
                comandoParaConsulta.Parameters.AddWithValue(parejaValores.Key, parejaValores.Value);
            }
            conexion.Open();
            try
            {
                comandoParaConsulta.ExecuteNonQuery();
                exito = true;
            }
            catch(System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                exito = false;
            }
            conexion.Close();
            return exito;
        }

        public bool EliminarEnBaseDatos(string consulta, Dictionary<string, object> valoresParametros)
        {
            bool exito;
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);

            foreach (KeyValuePair<string, object> parejaValores in valoresParametros)
            {
                comandoParaConsulta.Parameters.AddWithValue(parejaValores.Key, parejaValores.Value);
            }

            conexion.Open();
            
            try
            {
                comandoParaConsulta.ExecuteNonQuery();
                exito = true;
            }
            catch(System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                exito = false;
            }

            conexion.Close();
            return exito;
        }

        public bool ActualizarEnBaseDatos(string consulta, Dictionary<string, object> valoresParametros)
        {
            bool exito;
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);

            foreach (KeyValuePair<string, object> parejaValores in valoresParametros)
            {
                comandoParaConsulta.Parameters.AddWithValue(parejaValores.Key, parejaValores.Value);
            }

            conexion.Open();
            exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();

            return exito;
        }

        public Tuple<byte[],string> ObtenerArchivo (string consulta, KeyValuePair<string,object> parametro, string columnaContenido, string columnaTipo)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            comandoParaConsulta.Parameters.AddWithValue(parametro.Key, parametro.Value);
            
            conexion.Open();
            SqlDataReader lectorDeDatos = comandoParaConsulta.ExecuteReader();
            lectorDeDatos.Read();
            byte[] bytes = (byte[])lectorDeDatos[columnaContenido];
            string tipo = tipo = lectorDeDatos[columnaTipo].ToString();
            conexion.Close();

            return new Tuple<byte[], string>(bytes, tipo);
        }
    }
}