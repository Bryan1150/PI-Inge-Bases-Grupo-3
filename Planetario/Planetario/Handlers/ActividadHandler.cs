using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Planetario.Models;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data.SqlTypes;
using System.Web;

namespace Planetario.Handlers
{
    public class ActividadHandler : BaseDatosHandler
    {
        private List<ActividadModel> ConvertirTablaALista(DataTable tabla)
        {
            List<ActividadModel> actividades = new List<ActividadModel>();
            foreach (DataRow columna in tabla.Rows)
            {
                actividades.Add(
                    new ActividadModel
                    {
                        NombreActividad = Convert.ToString(columna["nombreActividadPK"]),
                        Descripcion = Convert.ToString(columna["descripcion"]),
                        Duracion = Convert.ToInt32(columna["duracionMins"]),
                        Complejidad = Convert.ToString(columna["complejidad"]),
                        PrecioAproximado = Convert.ToDouble(columna["precioAprox"]),
                        Categoria = Convert.ToString(columna["categoriaActividad"]),
                        DiaSemana = Convert.ToString(columna["diaSemana"]),
                        Fecha = Convert.ToString(columna["fechaActividad"]).Split()[0],
                        PropuestoPor = Convert.ToString(columna["propuestoPorFK"]),
                        PublicoDirigido = Convert.ToString(columna["publicoDirigidoActividad"]),
                        Tipo = Convert.ToString(columna["tipo"]),
                        Link = Convert.ToString(columna["link"])
                    });
            }
            return actividades;
        }

        private List<ActividadModel> ObtenerActividades(string consulta)
        {
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<ActividadModel> lista = ConvertirTablaALista(tabla);
            return lista;
        }

        public List<ActividadModel> ObtenerTodasLasActividades()
        {
            string consulta = "SELECT * FROM Actividad";
            return (ObtenerActividades(consulta));
        }

        public List<ActividadModel> ObtenerActividadesAprobadas()
        {
            string consulta  = "SELECT * FROM Actividad WHERE aprobado = 1";
            return (ObtenerActividades(consulta));
        }

        public List<ActividadModel> ObtenerActividadesRecomendadas(string publicoDirigido, string complejidad)
        {
            string consulta = "SELECT * FROM Actividad WHERE aprobado = 1 AND publicoDirigidoActividad = '" + publicoDirigido + "'AND complejidad = '" + complejidad + "';"; ;
            return (ObtenerActividades(consulta));
        }

        public List<ActividadModel> ObtenerActividadesPorBusqueda(string busqueda)
        {
            string consulta = "SELECT * FROM Actividad WHERE nombreActividadPK LIKE '%" + busqueda + "%' OR categoriaActividad LIKE '%" + busqueda + "%' OR tipo LIKE '%" + busqueda + "%';";
            return (ObtenerActividades(consulta));
        }

        public ActividadModel ObtenerActividad(string nombre)
        {
            string consulta = "Select * FROM Actividad WHERE nombreActividadPK = '" + nombre + "';";
            return (ObtenerActividades(consulta)[0]);
        }

        public bool InsertarActividad(ActividadModel actividad)
        {
            string consulta =
                "INSERT INTO Actividad (nombreActividadPK, descripcion, " +
                "duracionMins, complejidad, precioAprox, categoriaActividad, fechaActividad, propuestoPorFK, publicoDirigidoActividad, tipo, link) " +
                " VALUES ( @nombreActividadPK, @descripcion, @duracionMins, @complejidad, " +
                "@precioAprox, @categoria, @fecha, @propuestoPorFK, @publicoDirigidoActividad, @tipo, @link)";

            if (actividad.Link == null) { actividad.Link = ""; }

            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@nombreActividadPK", actividad.NombreActividad },
                {"@descripcion", actividad.Descripcion },
                {"@duracionMins", actividad.Duracion },
                {"@complejidad", actividad.Complejidad },
                {"@precioAprox", actividad.PrecioAproximado },
                {"@categoria", actividad.Categoria },
                {"@fecha", actividad.Fecha},
                {"@propuestoPorFK", HttpContext.Current.User.Identity.Name },
                {"@publicoDirigidoActividad", actividad.PublicoDirigido },
                {"@tipo", actividad.Tipo },
                {"@link", actividad.Link }
            };
            return (InsertarEnBaseDatos(consulta, valoresParametros));
        }

        public bool InsertarTopico(string nombreActividad, string topico)
        {
            string consulta = "INSERT INTO ActividadTopicos (NombreActividadFK, topicosActividad) "
                + " VALUES (@NombreActividadFK, @topicosActividad)";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@NombreActividadFK", nombreActividad },
                {"@topicosActividad", topico }
            };
            return (InsertarEnBaseDatos(consulta, valoresParametros));
        }

        public List<string> ObtenerTopicosActividad(string nombre)
        {
            string consulta = "SELECT topicosActividad FROM ActividadTopicos WHERE nombreActividadFK = '" + nombre + "';";
            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> topicos = new List<string>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                topicos.Add(Convert.ToString(fila["topicosActividad"]));
            }
            return topicos;
        }   
    }
}