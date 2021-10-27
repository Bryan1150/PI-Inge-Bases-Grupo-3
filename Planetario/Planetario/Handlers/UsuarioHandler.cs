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
        private readonly BaseDatosHandler BaseDatos;
        private string Consulta;

        public UsuarioHandler()
        {
            BaseDatos = new BaseDatosHandler();
        }

        public List<UsuarioModel> obtenerUsuarios()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();
            Consulta = "SELECT * FROM Usuario";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);
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
            bool exito;
            Consulta = "INSERT INTO dbo.Usuario (nombre, apellido1, apellido2, contrasena, correoPK, rolIdFK) " +
                "VALUES (@nombre, @apellido1, @apellido2, @contrasena, @correoPK, @rolIdFK) ";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@nombre",     usuarioNuevo.nombre },
                { "@apellido1",  usuarioNuevo.apellidoUno },
                { "@contrasena", usuarioNuevo.contrasena },
                { "@correoPK",   usuarioNuevo.correo },
                { "@rolIdFK",    usuarioNuevo.rolId }
            };
            if (usuarioNuevo.apellidoDos == null)
            {
                valoresParametros.Add("@apellido2", "");
            }
            else
            {
                valoresParametros.Add("@apellido2", usuarioNuevo.apellidoDos);
            }

            exito = BaseDatos.InsertarEnBaseDatos(Consulta, valoresParametros);
            return exito;
        }

        public bool esUsuarioValido(string contrasena, string correo)
        {
            bool esValido = false;
            string contrasenaUsuario;

            Consulta = "SELECT * FROM Usuario WHERE correoPK = '" + correo + "'";

            DataTable tablaResultados = BaseDatos.LeerBaseDeDatos(Consulta);

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