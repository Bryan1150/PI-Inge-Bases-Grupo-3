using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Planetario.Models;

namespace Planetario.Handlers
{
    public class ActividadHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public ActividadHandler()
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

        public bool crearActividad(ActividadModel actividad)
        {
            string consulta = "INSERT INTO Actividad (nombre, tema, descripcion, tipo, publicoDirigido, duracion, cantidadAsistentes, correo) "
                + "VALUES (@nombre, @tema, @descripcion, @tipo, @publicoDirigido, @duracion, @cantidadAsistentes, @correoFK) ";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            comandoParaConsulta.Parameters.AddWithValue("@nombre", actividad.nombre);
            comandoParaConsulta.Parameters.AddWithValue("@tema", actividad.tema);
            comandoParaConsulta.Parameters.AddWithValue("@descripcion", actividad.descripcion);
            comandoParaConsulta.Parameters.AddWithValue("@tipo", actividad.tipo);
            comandoParaConsulta.Parameters.AddWithValue("@publicoDirigido", actividad.publicoDirigido);
            comandoParaConsulta.Parameters.AddWithValue("@duracion", actividad.duracion);
            comandoParaConsulta.Parameters.AddWithValue("@cantidadAsistentes", actividad.cantidadAsistentes);
            comandoParaConsulta.Parameters.AddWithValue("@correoFK", actividad.correoFK);
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }

        public List<ActividadModel> obtenerTodasLasActividades()
        {
            List<ActividadModel> actividades = new List<ActividadModel>();
            string consulta = "SELECT * FROM Actividad ";
            DataTable tablaResultado = crearTablaConsulta(consulta);

            foreach(DataRow columna in tablaResultado.Rows)
            {
                actividades.Add(
                    new ActividadModel
                    {
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
    }
}