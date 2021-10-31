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
            List<string> NoticiaTopicos;
            Consulta = "SELECT idNoticiaPK, titulo, cuerpo, CONVERT(VARCHAR(20), fecha, 1) AS 'fecha', correoFuncionarioAutorFK, categoriaNoticia FROM Noticia ORDER BY fecha DESC";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);
            DataTable tablaResultadoTopicos;

            foreach (DataRow columna in tablaResultado.Rows)
            {
                Consulta = "SELECT DISTINCT * FROM dbo.NoticiaTopicos WHERE idNoticiaFK = " + Convert.ToInt32(columna["idNoticiaPK"]);
                tablaResultadoTopicos = BaseDatos.LeerBaseDeDatos(Consulta);
                NoticiaTopicos = new List<string>();
                string topico1 = "NULL";
                string topico2 = "NULL";
                string topico3 = "NULL";

                foreach (DataRow columna2 in tablaResultadoTopicos.Rows)
                {
                    NoticiaTopicos.Add(Convert.ToString(columna2["topicosNoticia"]));
                }

                for (int i = 0; i < NoticiaTopicos.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            topico1 = NoticiaTopicos[i];
                            break;
                        case 1:
                            topico2 = NoticiaTopicos[i];
                            break;
                        case 2:
                            topico3 = NoticiaTopicos[i];
                            break;
                    }
                }

                noticias.Add(
                    new NoticiaModel
                    {
                        id = Convert.ToInt32(columna["idNoticiaPK"]),
                        categoriaNoticia = Convert.ToString(columna["categoriaNoticia"]),
                        titulo = Convert.ToString(columna["titulo"]),
                        cuerpo = Convert.ToString(columna["cuerpo"]),
                        correoAutor = Convert.ToString(columna["correoFuncionarioAutorFK"]),
                        fecha = Convert.ToString(tablaResultado.Rows[0]["fecha"]),
                        topicoNoticia = topico1,
                        topicoNoticia2 = topico2,
                        topicoNoticia3 = topico3,
                    });
            }
            return noticias;
        }

        public List<String> ObtenerCategorias()
        {
            List<String> categorias = new List<String>();
            Consulta = "SELECT DISTINCT categoriaNoticia FROM dbo.Noticia";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                categorias.Add(Convert.ToString(columna["categoriaNoticia"]));
            }

            return categorias;
        }

        public bool crearNoticia(NoticiaModel noticia)
        {
            bool exito;
            Consulta =
            "INSERT INTO dbo.Noticia(titulo, cuerpo, fecha, correoFuncionarioAutorFK, categoriaNoticia) VALUES(@titulo, @cuerpo, @fecha, @correo, @categoria);" +
            "DECLARE @identity int = scope_identity();" +
            "INSERT INTO dbo.NoticiaTopicos(idNoticiaFK, topicosNoticia) VALUES(@identity, @topicoNoticia);";
            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@topicoNoticia",     noticia.topicoNoticia },
                { "@categoriaNoticia",  noticia.categoriaNoticia },
                { "@titulo",            noticia.titulo },
                { "@cuerpo",            noticia.cuerpo },
                { "@correo",            HttpContext.Current.User.Identity.Name},
                { "@fecha",             noticia.fecha}

            };

            if (noticia.topicoNoticia2 != "-Topico-")
            {
                valoresParametros.Add("@topicoNoticia2", noticia.topicoNoticia2);
                Consulta += "INSERT INTO dbo.NoticiaTopicos(idNoticiaFK, topicosNoticia) VALUES(@identity, @topicoNoticia2);";
            }
            else
            {
                valoresParametros.Add("@topicoNoticia2", "NULL");
            }

            if (noticia.topicoNoticia3 != "-Topico-")
            {
                valoresParametros.Add("@topicoNoticia3", noticia.topicoNoticia3);
                Consulta += "INSERT INTO dbo.NoticiaTopicos(idNoticiaFK, topicosNoticia) VALUES(@identity, @topicoNoticia3);";
            }
            else
            {
                valoresParametros.Add("@topicoNoticia3", "NULL");
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

        public NoticiaModel buscarNoticia(string stringId)
        {
            Consulta = "SELECT idNoticiaPK, titulo, cuerpo, CONVERT(VARCHAR(20), fecha, 1) AS 'fecha', correoFuncionarioAutorFK FROM Noticia WHERE idNoticiaPK = " + stringId + ";";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);
            NoticiaModel resultado = null;
            if (tablaResultado.Rows[0] != null)
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

    }
}