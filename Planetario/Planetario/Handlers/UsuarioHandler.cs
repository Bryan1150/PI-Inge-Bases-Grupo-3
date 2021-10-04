using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace Planetario.Handlers
{

    public class UsuarioHandler
    {
        private SqlConnection conexion;
        private readonly string rutaConexion;

        public UsuarioHandler()
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

        public List<UsuarioModel> obtenerUsuarios()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();
            string consulta = "SELECT * FROM Usuario";
            DataTable tablaResultado = crearTablaConsulta(consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                usuarios.Add(
                new UsuarioModel
                {
                    nombre = Convert.ToString(columna["nombre"]),
                    apellidoUno = Convert.ToString(columna["apellido1"]),
                    apellidoDos = Convert.ToString(columna["apellido2"]),
                    contrasena = Convert.ToString(columna["contrasena"]),
                    correo = Convert.ToString(columna["correoPK"])
                });
            }
            return usuarios;
        }

        public bool insertarUsuario(UsuarioModel usuarioNuevo)
        {
            string consulta = "INSERT INTO dbo.Usuario (nombre, apellido1, apellido2, contrasena, correoPK, rolIdFK) " +
                "VALUES (@nombre, @apellido1, @apellido2, @contrasena, @correoPK, @rolIdFK) ";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@nombre", usuarioNuevo.nombre);
            comandoParaConsulta.Parameters.AddWithValue("@apellido1", usuarioNuevo.apellidoUno);
            if (usuarioNuevo.apellidoDos == null)
            {
                comandoParaConsulta.Parameters.AddWithValue("@apellido2", SqlBinary.Null);
            }
            else
            {
                comandoParaConsulta.Parameters.AddWithValue("@apellido2", usuarioNuevo.apellidoDos);
            }
            comandoParaConsulta.Parameters.AddWithValue("@contrasena", usuarioNuevo.contrasena);
            comandoParaConsulta.Parameters.AddWithValue("@correoPK", usuarioNuevo.correo);
            comandoParaConsulta.Parameters.AddWithValue("@rolIdFK", 4);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();

            return exito;
        }

        public bool esUsuarioValido(string contrasena, string correo)
        {
            bool esValido = false;
            string contrasenaUsuario;

            string consulta = "SELECT * FROM Usuario WHERE correoPK = '" + correo + "'";

            DataTable tablaResultados = crearTablaConsulta(consulta);

            if (tablaResultados.Rows.Count > 0)
            {
                foreach (DataRow columna in tablaResultados.Rows)
                {
                    contrasenaUsuario = Convert.ToString(columna["contrasena"]);

                    if (contrasenaUsuario == contrasena) { esValido = true; }
                }
            }
            return esValido;
        }
    }
}