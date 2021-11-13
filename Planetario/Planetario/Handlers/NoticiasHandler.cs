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
    public class NoticiasHandler : BaseDatosHandler
    {
        public List<NoticiaModel> obtenerTodasLasNoticias()
        {
            List<NoticiaModel> noticias = new List<NoticiaModel>();
            String Consulta = "SELECT * FROM Noticia ORDER BY fecha DESC";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            foreach (DataRow columna in tablaResultado.Rows)
                noticias.Add(
                new NoticiaModel
                {
                    id = Convert.ToInt32(columna["idNoticiaPK"]),
                    Titulo = Convert.ToString(columna["titulo"]),
                    Cuerpo = Convert.ToString(columna["cuerpo"]),
                    CorreoAutor = Convert.ToString(columna["correoFuncionarioAutorFK"]),
                    Fecha = Convert.ToString(tablaResultado.Rows[0]["fecha"]),
                    CategoriaNoticia = Convert.ToString(columna["categoriaNoticia"])
                });
            return noticias;
        }

        public IList<string> obtenerTopicos(String idNoticiaPK)
        {
            string consulta = "SELECT TN.topicosNoticia FROM NoticiaTopicos TN WHERE TN.idNoticiaFK = '" + idNoticiaPK + "';";
            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> topicos = new List<string>();
            foreach (DataRow fila in tablaResultados.Rows)
            {
                topicos.Add(Convert.ToString(fila["topicosNoticia"]));
            }
            return topicos;
        }

        public bool crearNoticia(NoticiaModel noticia)
        {
            bool exito;
            String Consulta =
            "INSERT INTO dbo.Noticia(titulo, cuerpo, fecha, correoFuncionarioAutorFK, categoriaNoticia, Imagen1, tipoImagen1, tipoImagen2) VALUES(@titulo, @cuerpo, @fecha, @correo, @categoria, " +
            "@imagen1, @tipoImagen1, @imagen2, @tipoImagen2);" +
            "DECLARE @identity int = scope_identity();" +
            "INSERT INTO dbo.NoticiaTopicos(idNoticiaFK, topicosNoticia) VALUES(@identity, @topicoNoticia);";
            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@topicoNoticia",     noticia.TopicosNoticia },
                { "@categoriaNoticia",  noticia.CategoriaNoticia },
                { "@titulo",            noticia.Titulo },
                { "@cuerpo",            noticia.Cuerpo },
                { "@correo",            HttpContext.Current.User.Identity.Name},
                { "@fecha",             noticia.Fecha},
                { "@imagen1",           noticia.Imagen1},
                { "@imagen2",           noticia.Imagen2},
                { "@tipoImagen1",       noticia.TipoImagen1},
                { "@tipoImagen2",       noticia.TipoImagen2}
            };
            exito = InsertarEnBaseDatos(Consulta, valoresParametros);
            return exito;
        }

        public Tuple<byte[], string> ObtenerFoto(string numNoticia)
        {
            string nombreArchivo = "imagen", tipoArchivo = "tipoImagen";
            String Consulta  = "SELECT " + nombreArchivo + ", " + tipoArchivo + " FROM Noticia WHERE idNoticiaPK = @id";
            int id = Int32.Parse(numNoticia);

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@id",             id },
            };

            return ObtenerArchivo(Consulta, valoresParametros, nombreArchivo, tipoArchivo);
        }

        public NoticiaModel buscarNoticia(string stringId)
        {
            String Consulta  = "SELECT * FROM Noticia WHERE idNoticiaPK = '" + stringId + "';";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            NoticiaModel resultado = null;
            if (tablaResultado.Rows[0] != null)
            {
                resultado = new NoticiaModel
                {
                    id = Convert.ToInt32(tablaResultado.Rows[0]["idNoticiaPK"]),
                    Titulo = Convert.ToString(tablaResultado.Rows[0]["titulo"]),
                    Cuerpo = Convert.ToString(tablaResultado.Rows[0]["cuerpo"]),
                    CorreoAutor = Convert.ToString(tablaResultado.Rows[0]["correoFuncionarioAutorFK"]),
                    Fecha = Convert.ToString(tablaResultado.Rows[0]["fecha"]),
                    CategoriaNoticia = Convert.ToString(tablaResultado.Rows[0]["categoriaNoticia"])
                };
            }
            return resultado;
        }

    }
}