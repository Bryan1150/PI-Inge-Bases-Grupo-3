using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

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

        private byte[] obtenerBytes(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            BinaryReader lector = new BinaryReader(archivo.InputStream); //
            bytes = lector.ReadBytes(archivo.ContentLength);
            return bytes;
        }

        public bool crearNoticia(NoticiaModel noticia)
        {
            string consulta = "INSERT INTO Noticia (titulo, cuerpo, fecha, correoFuncionarioAutorFK , imagen, tipoImagen)" +
            "VALUES (@tituloN,@cuerpoN,@fechaN,@correoN,@imagenN,@tipoImagenN) ";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@tituloN", noticia.titulo);
            comandoParaConsulta.Parameters.AddWithValue("@cuerpoN", noticia.cuerpo);
            comandoParaConsulta.Parameters.AddWithValue("@fechaN", noticia.fecha); 
            comandoParaConsulta.Parameters.AddWithValue("@correoN", noticia.correoAutor);

            if (noticia.imagen == null)
            {
                comandoParaConsulta.Parameters.AddWithValue("@imagenN", SqlBinary.Null);
                comandoParaConsulta.Parameters.AddWithValue("@tipoImagenN", SqlBinary.Null);
            }
            else 
            {
                comandoParaConsulta.Parameters.AddWithValue("@imagenN", obtenerBytes(noticia.imagen));
                comandoParaConsulta.Parameters.AddWithValue("@tipoImagenN", noticia.imagen.ContentType);
            }
            

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }

        public Tuple<byte[], string> ObtenerFoto(string numNoticia)
        {
            byte[] bytes;
            string contentType;
            string consulta = "SELECT imagen, tipoImagen FROM Noticia WHERE idNoticiaPK = @id";
            int id = Int32.Parse(numNoticia);
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            comandoParaConsulta.Parameters.AddWithValue("@id", id);
            conexion.Open();

            SqlDataReader lectorDeDatos = comandoParaConsulta.ExecuteReader();
            lectorDeDatos.Read();

            bytes = (byte[])lectorDeDatos["imagen"];
            contentType = lectorDeDatos["tipoImagen"].ToString();

            conexion.Close();
            return new Tuple<byte[], string>(bytes, contentType);
        }


    }
}