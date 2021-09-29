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
            string Consulta = "SELECT M.idMaterialPk, M.titulo, CONVERT(VARCHAR(20), M.fechaSubida, 1) AS fechaSubida , M.correoResponsableFK, " +
                "M.idioma, M.autor, U.nombre + ' ' + U.apellido1 + ' ' + U.apellido2 AS 'nombreResponsable' " +
                "FROM MaterialEducativo M " +
                "JOIN Usuario U ON M.correoResponsableFK = U.correoPK;";
            DataTable tablaResultado = CrearTablaConsulta(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                ListaMateriales.Add(
                    new MaterialEducativoModel
                    {
                        Id = Convert.ToInt32(columna["idMaterial"]),
                        Titulo = Convert.ToString(columna["titulo"]),
                        Fecha  = Convert.ToString(columna["fechaSubida"]),
                        CorreoResponsable = Convert.ToString(columna["correoResponsable"]),
                        Idioma = Convert.ToString(columna["idioma"]),
                        Autor = Convert.ToString(columna["autor"]),
                        NombreResponsable = Convert.ToString(columna["nombreResponsable"]),
                    }
                );
            }

            return ListaMateriales;
        }

        public Tuple<byte[], string> ObtenerVistaPrevia(int id)
        {
            byte[] bytes;
            string contentType;
            string consulta = "SELECT imagenVistaPrevia, tipoArchivoVistaPrevia FROM MaterialEducativo WHERE idMaterialPk = @id";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, Conexion);
            comandoParaConsulta.Parameters.AddWithValue("@id", id);
            Conexion.Open();

            SqlDataReader lectorDeDatos = comandoParaConsulta.ExecuteReader();
            lectorDeDatos.Read();

            bytes = (byte[])lectorDeDatos["foto"];
            contentType = lectorDeDatos["tipoArchivoFoto"].ToString();

            Conexion.Close();
            return new Tuple<byte[], string>(bytes, contentType);
        }

    }
}