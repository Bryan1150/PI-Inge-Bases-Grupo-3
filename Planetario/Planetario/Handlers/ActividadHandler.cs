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
            Consulta = "INSERT INTO Actividad (nombre, tema, descripcion, tipo, publicoDirigido, duracion, correoFK) "
                + "VALUES (@nombre, @tema, @descripcion, @tipo, @publicoDirigido, @duracion, @correoFK) ";
            
            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@nombre", actividad.nombre },
                {"@tema", actividad.tema },
                {"@descripcion", actividad.descripcion },
                {"@tipo", actividad.tipo },
                {"@publicoDirigido", actividad.publicoDirigido },
                {"@duracion", actividad.duracion },
                {"@correoFK", actividad.correoFK }
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
                    new ActividadModel
                    {
                        id = Convert.ToInt32(columna["idActividadPK"]),
                        nombre = Convert.ToString(columna["nombre"]),
                        tema = Convert.ToString(columna["tema"]),
                        descripcion = Convert.ToString(columna["descripcion"]),
                        tipo = Convert.ToString(columna["tipo"]),
                        publicoDirigido = Convert.ToString(columna["publicoDirigido"]),
                        duracion = Convert.ToInt32(columna["duracion"]),
                        correoFK = Convert.ToString(columna["correoFK"])
                    });
            }
            return actividades;
        }

        public ActividadModel buscarActividad(string stringId)
        {
            Consulta = "SELECT * FROM Actividad WHERE idActividadPK = " + stringId + ";";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);
            ActividadModel resultado = null;
            if (tablaResultado.Rows[0] != null)
            {
                resultado = new ActividadModel
                {
                    id = Convert.ToInt32(tablaResultado.Rows[0]["idActividadPK"]),
                    nombre = Convert.ToString(tablaResultado.Rows[0]["nombre"]),
                    tema = Convert.ToString(tablaResultado.Rows[0]["tema"]),
                    descripcion = Convert.ToString(tablaResultado.Rows[0]["descripcion"]),
                    tipo = Convert.ToString(tablaResultado.Rows[0]["tipo"]),
                    publicoDirigido = Convert.ToString(tablaResultado.Rows[0]["publicoDirigido"]),
                    duracion = Convert.ToInt32(tablaResultado.Rows[0]["duracion"]),
                    correoFK = Convert.ToString(tablaResultado.Rows[0]["correoFK"])
                };
            }
            return resultado;
        }

        public List<ActividadModel> obtenerActividadBuscada(string palabra)
        {
            List<ActividadModel> actividadesUnicas = new List<ActividadModel>();
            Consulta = "SELECT * FROM Actividad WHERE nombre LIKE '%" + palabra + "%'";

            DataTable TablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                actividadesUnicas.Add(
                    new ActividadModel
                    {
                        id = Convert.ToInt32(columna["idActividadPK"]),
                        nombre = Convert.ToString(columna["nombre"]),
                        tema = Convert.ToString(columna["tema"]),
                        descripcion = Convert.ToString(columna["descripcion"]),
                        tipo = Convert.ToString(columna["tipo"]),
                        publicoDirigido = Convert.ToString(columna["publicoDirigido"]),
                        duracion = Convert.ToInt32(columna["duracion"]),
                        correoFK = Convert.ToString(columna["correoFK"])
                    });
            }
            return actividadesUnicas;
        }

    }
}