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
        private readonly BaseDatosHandler BaseDatos;
        private string Consulta;

        public NoticiasHandler()
        {
            BaseDatos = new BaseDatosHandler();
        }

        public List<NoticiaModel> obtenerTodasLasNoticias()
        {
            List<NoticiaModel> noticias = new List<NoticiaModel>();
            Consulta = "SELECT idNoticiaPK, titulo, cuerpo, CONVERT(VARCHAR(20), fecha, 1) AS 'fecha', correoFuncionarioAutorFK FROM Noticia ORDER BY fecha DESC";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

            foreach(DataRow columna in tablaResultado.Rows)
            {
                noticias.Add(
                    new NoticiaModel
                    {
                        id = Convert.ToInt32(columna["idNoticiaPK"]),
                        titulo = Convert.ToString(columna["titulo"]),
                        cuerpo = Convert.ToString(columna["cuerpo"]),
                        fecha = Convert.ToString(columna["fecha"]),
                        correoAutor = Convert.ToString(columna["correoFuncionarioAutorFK"]),
                    });
            }
            return noticias;
        }

        public NoticiaModel buscarNoticia(string stringId)
        {
            Consulta = "SELECT idNoticiaPK, titulo, cuerpo, CONVERT(VARCHAR(20), fecha, 1) AS 'fecha', correoFuncionarioAutorFK FROM Noticia WHERE idNoticiaPK = " + stringId + ";";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);
            NoticiaModel resultado = null;
            if(tablaResultado.Rows[0] != null)
            {
                resultado = new NoticiaModel
                {
                    id = Convert.ToInt32(tablaResultado.Rows[0]["idNoticiaPK"]),
                    titulo = Convert.ToString(tablaResultado.Rows[0]["titulo"]),
                    cuerpo = Convert.ToString(tablaResultado.Rows[0]["cuerpo"]),
                    fecha = Convert.ToString(tablaResultado.Rows[0]["fecha"]),
                    correoAutor = Convert.ToString(tablaResultado.Rows[0]["correoFuncionarioAutorFK"]),
                };
            }
            return resultado;
        }

        public bool crearNoticia(NoticiaModel noticia)
        {
            FileHandler manejadorDeArchivos = new FileHandler();
            bool exito;
            Consulta = "INSERT INTO Noticia (titulo, cuerpo, fecha, correoFuncionarioAutorFK , imagen, tipoImagen)" +
            "VALUES (@tituloN,@cuerpoN,@fechaN,@correoN,@imagenN,@tipoImagenN) ";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@tituloN", noticia.titulo },
                { "@cuerpoN", noticia.cuerpo },
                { "@fechaN", noticia.fecha },
                { "@correoN", noticia.correoAutor }
            };

            if (noticia.imagen == null)
            {
                valoresParametros.Add("@imagenN", SqlBinary.Null);
                valoresParametros.Add("@tipoImagenN", SqlBinary.Null);
            }
            else 
            {
                valoresParametros.Add("@imagenN", manejadorDeArchivos.ConvertirArchivoABytes(noticia.imagen));
                valoresParametros.Add("@tipoImagenN", noticia.imagen.ContentType);
            }

            exito = BaseDatos.InsertarEnBaseDatos(Consulta, valoresParametros);

            return exito;
        }

        public Tuple<byte[], string> ObtenerFoto(string numNoticia)
        {
            string nombreArchivo = "imagen", tipoArchivo = "tipoImagen";
            Consulta = "SELECT "+ nombreArchivo +", " + tipoArchivo + " FROM Noticia WHERE idNoticiaPK = @id";
            int id = Int32.Parse(numNoticia);

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@id",             id },
            };

            return BaseDatos.ObtenerArchivo(Consulta, valoresParametros, nombreArchivo, tipoArchivo);
        }


    }
}