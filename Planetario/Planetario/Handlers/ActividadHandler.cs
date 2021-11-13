using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Planetario.Models;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data.SqlTypes;

namespace Planetario.Handlers
{
    public class ActividadHandler : BaseDatosHandler
    {
        public bool crearActividad(ActividadModel actividad)
        {
            bool exito;
            string Consulta = "INSERT INTO Actividad (nombreActividadPK, descripcion, " +
                "duracionMins, complejidad, precioAprox, categoriaActividad, diaSemana, propuestoPorFK, publicoDirigidoActividad, tipo) "
                + "VALUES ( @nombreActividadPK, @descripcion, @duracionMins, @complejidad, " +
                "@precioAprox, @categoriaActividad, @diaSemana, @propuestoPorFK, @publicoDirigidoActividad, @tipo) ";
            
            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@nombreActividadPK", actividad.NombreActividad },
                {"@descripcion", actividad.Descripcion },
                {"@duracionMins", actividad.Duracion },
                {"@complejidad", actividad.Complejidad },
                {"@precioAprox", actividad.PrecioAproximado },
                {"@categoria", actividad.Categoria },
                {"@diaSemana", actividad.DiaSemana},
                {"@propuestoPorFK", actividad.PropuestoPor },
                {"@publicoDirigidoActividad", actividad.PublicoDirigido },
                {"@tipo", actividad.Tipo }
            };
            
            exito = InsertarEnBaseDatos(Consulta, valoresParametros);

            return exito;
        }

        public List<ActividadModel> obtenerTodasLasActividades()
        {
            List<ActividadModel> actividades = new List<ActividadModel>();
            string Consulta= "SELECT * FROM Actividad";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            foreach(DataRow columna in tablaResultado.Rows)
            {
                actividades.Add(
                    new ActividadModel {
                        NombreActividad = Convert.ToString(tablaResultado.Rows[0]["@nombreActividadPK"]),
                        Descripcion = Convert.ToString(tablaResultado.Rows[0]["@descripcion"]),
                        Duracion = Convert.ToInt32(tablaResultado.Rows[0]["@duracionMins"]),
                        Complejidad = Convert.ToString(tablaResultado.Rows[0]["@complejidad"]),
                        PrecioAproximado = Convert.ToDouble(tablaResultado.Rows[0]["@precioAprox"]),
                        Categoria = Convert.ToString(tablaResultado.Rows[0]["@categoria"]),
                        DiaSemana = Convert.ToString(tablaResultado.Rows[0]["@diaSemana"]),
                        PropuestoPor = Convert.ToString(tablaResultado.Rows[0]["@propuestoPorFK"]),
                        PublicoDirigido = Convert.ToString(tablaResultado.Rows[0]["@publicoDirigidoActividad"])
                    });
            }
            return actividades;
        }

        public List<ActividadModel> obtenerTodasLasActividadesAprobadas()
        {
            List<ActividadModel> actividades = new List<ActividadModel>();
            string Consulta  = "SELECT * FROM Actividad WHERE aprobado = 1";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            foreach (DataRow columna in tablaResultado.Rows)
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
                        PropuestoPor = Convert.ToString(columna["propuestoPorFK"]),
                        PublicoDirigido = Convert.ToString(columna["publicoDirigidoActividad"]),
                        Tipo = Convert.ToString(columna["tipo"])
                    });
            }
            return actividades;
        }

        public IList<string> obtenerTopicosActividades(string nombre)
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

        public ActividadModel buscarActividad(string nombre)
        {
            ActividadModel actividad = null;
            string Consulta = "Select * FROM Actividad WHERE nombreActividadPK = '" + nombre + "';";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            if (tablaResultado.Rows.Count >= 1)
            {
                actividad = new ActividadModel
                {
                    NombreActividad = Convert.ToString(tablaResultado.Rows[0]["nombreActividadPK"]),
                    Descripcion = Convert.ToString(tablaResultado.Rows[0]["descripcion"]),
                    Duracion = Convert.ToInt32(tablaResultado.Rows[0]["duracionMins"]),
                    Complejidad = Convert.ToString(tablaResultado.Rows[0]["complejidad"]),
                    PrecioAproximado = Convert.ToDouble(tablaResultado.Rows[0]["precioAprox"]),
                    Categoria = Convert.ToString(tablaResultado.Rows[0]["categoriaActividad"]),
                    DiaSemana = Convert.ToString(tablaResultado.Rows[0]["diaSemana"]),
                    PropuestoPor = Convert.ToString(tablaResultado.Rows[0]["propuestoPorFK"]),
                    PublicoDirigido = Convert.ToString(tablaResultado.Rows[0]["publicoDirigidoActividad"]),
                    Tipo = Convert.ToString(tablaResultado.Rows[0]["tipo"])
                };
            }
            return actividad;
        }

        public List<ActividadModel> obtenerActividadBuscada(string palabra)
        {
            List<ActividadModel> actividadesUnicas = new List<ActividadModel>();
            string Consulta  = "SELECT * FROM Actividad WHERE nombreActividadPK LIKE '%" + palabra + "%' OR categoriaActividad LIKE '%" + palabra + "%' OR tipo LIKE '%" + palabra + "%';";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                actividadesUnicas.Add(
                    new ActividadModel
                    {
                        NombreActividad = Convert.ToString(columna["nombreActividadPK"]),
                        Descripcion = Convert.ToString(columna["descripcion"]),
                        Duracion = Convert.ToInt32(columna["duracionMins"]),
                        Complejidad = Convert.ToString(columna["complejidad"]),
                        PrecioAproximado = Convert.ToDouble(columna["precioAprox"]),
                        Categoria = Convert.ToString(columna["categoriaActividad"]),
                        DiaSemana = Convert.ToString(columna["diaSemana"]),
                        PropuestoPor = Convert.ToString(columna["propuestoPorFK"]),
                        PublicoDirigido = Convert.ToString(columna["publicoDirigidoActividad"]),
                        Tipo = Convert.ToString(columna["tipo"])
                    });
            }
            return actividadesUnicas;
        }

        public List<FacturaModel> obtenerTodasLasFacturas(string nombreActividad)
        {
            List<FacturaModel> facturas = new List<FacturaModel>();
            string Consulta = "EXEC USP_ObtenerTodosPagos '" + nombreActividad + "';";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                facturas.Add(
                    new FacturaModel
                    {
                        ID = Convert.ToInt32(columna["idFacturaPK"]),
                        FechaCompra = Convert.ToString(columna["fechaCompra"]),
                        PagoTotal = Convert.ToDouble(columna["pagoTotal"]),
                        CorreoParticipante = Convert.ToString(columna["correoParticipanteFK"]),
                        NombreActividad = Convert.ToString(columna["nombreActividadFK"]),
                    });
            }
            return facturas;
        }
    }
}