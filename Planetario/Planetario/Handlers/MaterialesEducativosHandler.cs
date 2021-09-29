using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

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

        private byte[] ObtenerBytes(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            BinaryReader lector = new BinaryReader(archivo.InputStream);
            bytes = lector.ReadBytes(archivo.ContentLength);
            return bytes;
        }

        public bool AlmacenarMaterialEducativo(MaterialEducativoModel material)
        {
            string Consulta = "INSERT INTO MaterialEducativo " +
                "VALUES (@titulo, DATE(), @correoResponsable, @idioma, " +
                "@autor, @imagenVistaPrevia, @tipoArchivoVistaPrevia, " +
                "@archivo, @tipoArchivo );";

            SqlCommand ComandoParaConsulta = new SqlCommand(Consulta, Conexion);

            ComandoParaConsulta.Parameters.AddWithValue("@titulo", material.Titulo);
            ComandoParaConsulta.Parameters.AddWithValue("@correo", material.CorreoResponsable);
            ComandoParaConsulta.Parameters.AddWithValue("@idioma", material.Idioma);
            ComandoParaConsulta.Parameters.AddWithValue("@autor", material.Autor);
            ComandoParaConsulta.Parameters.AddWithValue("@imagenVistaPrevia", ObtenerBytes(material.ImagenVistaPrevia));
            ComandoParaConsulta.Parameters.AddWithValue("@tipoArchivoVistaPrevia", material.ImagenVistaPrevia.ContentType);
            ComandoParaConsulta.Parameters.AddWithValue("@archivo", ObtenerBytes(material.Archivo));
            ComandoParaConsulta.Parameters.AddWithValue("@tipoArchivo", material.Archivo.ContentType);

            Conexion.Open();
            bool exito = ComandoParaConsulta.ExecuteNonQuery() >= 1;
            Conexion.Close();
            return exito;
        }

    }
}