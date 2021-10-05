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
            bool HayVistaPrevia = material.HayVistaPrevia();
            string Consulta = "INSERT INTO MaterialEducativo " +
                "( titulo, fechaSubida, correoResponsableFK, publicoDirigido, ";
            if (HayVistaPrevia)
            {
                Consulta += "vistaPrevia, tipoArchivoVistaPrevia, ";
            }

            Consulta += "archivo, tipoArchivo ) " +
            "VALUES ( @titulo, GETDATE(), @correoResponsable, @publicoDirigido, ";

            if (HayVistaPrevia)
            {
                Consulta += "@imagenVistaPrevia, @tipoArchivoVistaPrevia, ";
            }
            Consulta += "@archivo, @tipoArchivo );";

            SqlCommand ComandoParaConsulta = new SqlCommand(Consulta, Conexion);

            ComandoParaConsulta.Parameters.AddWithValue("@titulo", material.Titulo);
            ComandoParaConsulta.Parameters.AddWithValue("@correoResponsable", material.CorreoResponsable);
            ComandoParaConsulta.Parameters.AddWithValue("@publicoDirigido", material.PublicoDirigido);

            if (HayVistaPrevia)
            {
                ComandoParaConsulta.Parameters.AddWithValue("@imagenVistaPrevia", ObtenerBytes(material.ImagenVistaPrevia));
                ComandoParaConsulta.Parameters.AddWithValue("@tipoArchivoVistaPrevia", material.ImagenVistaPrevia.ContentType);
            }

            ComandoParaConsulta.Parameters.AddWithValue("@archivo", ObtenerBytes(material.Archivo));
            ComandoParaConsulta.Parameters.AddWithValue("@tipoArchivo", material.Archivo.ContentType);

            Conexion.Open();
            bool exito = ComandoParaConsulta.ExecuteNonQuery() >= 1;
            Conexion.Close();
            return exito;
        }



        public List<MaterialEducativoModel> obtenerMateriales()
        {
            List<MaterialEducativoModel> material = new List<MaterialEducativoModel>();
            string consulta = "SELECT * FROM MaterialEducativo";
            DataTable tablaResultado = crearTablaConsulta(consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                material.Add(
                new MaterialEducativoModel
                {
                    Id = Convert.ToInt32(columna["idMaterialPK"]),
                    Titulo = Convert.ToString(columna["titulo"]),
                    Fecha = Convert.ToString(columna["fechaSubida"]),
                    CorreoResponsable = Convert.ToString(columna["correoResponsableFK"]),
                    PublicoDirigido = Convert.ToString(columna["publicoDirigido"]),
                });
            }
            return material;
        }

        private DataTable crearTablaConsulta(string consulta)
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