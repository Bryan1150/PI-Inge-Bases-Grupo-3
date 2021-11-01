using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Planetario.Models;

namespace Planetario.Handlers
{
    public class ActividadHandler
    {
        private readonly BaseDatosHandler BaseDatos;
        private string Consulta;

        public ActividadHandler()
        {
            BaseDatos = new BaseDatosHandler();
        }

        public bool crearActividad(ActividadModel actividad)
        {
            bool exito;
            Consulta = "INSERT INTO Actividad (nombreActividadPK, descripcion, " +
                "duracionMins, complejidad, precioAprox, categoriaActividad, diaSemana, propuestoPorFK, publicoDirigidoActividad) "
                + "VALUES ( @nombreActividadPK, @descripcion, @duracionMins, @complejidad, " +
                "@precioAprox, @categoriaActividad, @diaSemana, @propuestoPorFK, @publicoDirigidoActividad) ";
            
            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@nombreActividadPK", actividad.NombreActividad },
                {"@descripcion", actividad.Descripcion },
                {"@duracionMins", actividad.Duracion },
                {"@complejidad", actividad.Complejidad },
                {"@precioAprox", actividad.PrecioAproximado },
                {"@categoria", actividad.Categoria },
                {"@diaSemana", actividad.DiaSemana},
                {"@propuestoPorFK", actividad.PropuestoPor },
                {"@publicoDirigidoActividad", actividad.PublicoDirigido }
            };
            
            exito = BaseDatos.InsertarEnBaseDatos(Consulta, valoresParametros);

            return exito;
        }

        public List<ActividadModel> obtenerTodasLasActividades()
        {
            List<ActividadModel> actividades = new List<ActividadModel>();
            Consulta= "SELECT * FROM Actividad ";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

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

        public ActividadModel buscarActividad(string nombre)
        {
            Consulta = "SELECT * FROM Actividad WHERE nombreActividadPK = " + nombre + ";";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);
            ActividadModel resultado = null;
            if (tablaResultado.Rows[0] != null)
            {
                resultado = new ActividadModel
                {
                    NombreActividad = Convert.ToString(tablaResultado.Rows[0]["@nombreActividadPK"]),
                    Descripcion = Convert.ToString(tablaResultado.Rows[0]["@descripcion"]),
                    Duracion = Convert.ToInt32(tablaResultado.Rows[0]["@duracionMins"]),
                    Complejidad = Convert.ToString(tablaResultado.Rows[0]["@complejidad"]),
                    PrecioAproximado = Convert.ToDouble(tablaResultado.Rows[0]["@precioAprox"]),
                    Categoria = Convert.ToString(tablaResultado.Rows[0]["@categoria"]),
                    DiaSemana = Convert.ToString(tablaResultado.Rows[0]["@diaSemana"]),
                    PropuestoPor = Convert.ToString(tablaResultado.Rows[0]["@propuestoPorFK"]),
                    PublicoDirigido = Convert.ToString(tablaResultado.Rows[0]["@publicoDirigidoActividad"])
                };
            }
            return resultado;
        }

        public List<ActividadModel> obtenerActividadBuscada(string palabra)
        {
            List<ActividadModel> actividadesUnicas = new List<ActividadModel>();
            Consulta = "SELECT * FROM Actividad WHERE nombre LIKE '%" + palabra + "%'";

            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                actividadesUnicas.Add(
                    new ActividadModel
                    {
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
            return actividadesUnicas;
        }

    }
}