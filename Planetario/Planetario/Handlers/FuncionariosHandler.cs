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
    public class FuncionariosHandler : BaseDatosHandler
    {
        public List<FuncionarioModel> obtenerFuncionariosSimple()
        {
            List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();
            string consulta = "Select * FROM Funcionario";
            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                funcionarios.Add(
                new FuncionarioModel
                {
                    CorreoContacto = Convert.ToString(columna["correoPK"]),
                    Nombre = Convert.ToString(columna["nombre"]),
                    Apellido1 = Convert.ToString(columna["apellido1"]),
                    Apellido2 = Convert.ToString(columna["apellido2"]),
                    Descripcion = Convert.ToString(columna["descripcion"]),
                    Pais = Convert.ToString(columna["pais"]),
                    FechaIncorporacion = Convert.ToString(columna["fechaIncorporacion"]),
                    Genero = Convert.ToString(columna["genero"]),
                    AreaExpertis = Convert.ToString(columna["areaExpertis"])
                });
            }
            return funcionarios;
        }

        public FuncionarioModel buscarFuncionario(string correo)
        {
            FuncionarioModel funcionario = null;
            string consulta = "Select * FROM Funcionario WHERE correoPK = '" + correo + "';";
            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            if(tablaResultado.Rows[0] != null)
            {
                funcionario = new FuncionarioModel
                {
                    CorreoContacto = Convert.ToString(tablaResultado.Rows[0]["correoPK"]),
                    Nombre = Convert.ToString(tablaResultado.Rows[0]["nombre"]),
                    Apellido1 = Convert.ToString(tablaResultado.Rows[0]["apellido1"]),
                    Apellido2 = Convert.ToString(tablaResultado.Rows[0]["apellido2"]),
                    Descripcion = Convert.ToString(tablaResultado.Rows[0]["descripcion"]),
                    Pais = Convert.ToString(tablaResultado.Rows[0]["pais"]),
                    FechaIncorporacion = Convert.ToString(tablaResultado.Rows[0]["fechaIncorporacion"]),
                    Genero = Convert.ToString(tablaResultado.Rows[0]["genero"]),
                    AreaExpertis = Convert.ToString(tablaResultado.Rows[0]["areaExpertis"])
                };
            }
            return funcionario;
        }

        public IList<string> obtenerIdiomasFuncionario(string correo) 
        {
            string consulta = "SELECT FI.idioma FROM FuncionarioIdioma FI WHERE FI.correoFuncionarioFK = '" + correo + "';";
            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> idiomas = new List<string>();

            foreach(DataRow fila in tablaResultados.Rows)
            {
                idiomas.Add(Convert.ToString(fila["idioma"]));
            }

            return idiomas;
        }

        public IList<string> obtenerTitulosFuncionario(string correo) { 
            string consulta = "SELECT FT.titulo FROM FuncionarioTitulo FT WHERE FT.correoFuncionarioFK = '" + correo + "';";
            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> titulos = new List<string>();

            foreach(DataRow fila in tablaResultados.Rows)
            {
                titulos.Add(Convert.ToString(fila["titulo"]));
            }
            return titulos;
        }

        public IList<string> obtenerRolesFuncionario(string correo) { 
            string consulta = "SELECT FR.rol FROM FuncionarioRol FR WHERE FR.correoFuncionarioFK = '" + correo + "';";
            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> roles = new List<string>();

            foreach(DataRow fila in tablaResultados.Rows)
            {
                roles.Add(Convert.ToString(fila["rol"]));
            }
            return roles;
        }

        public bool esFuncionarioValido(string contrasena, string correo)
        {
            bool esValido = false;
            string contrasenaFuncionario;

            string consulta = "SELECT correoFuncionarioFK, contraseña FROM Credenciales WHERE correoFuncionarioFK = '" + correo + "'";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);

            if (tablaResultados.Rows.Count > 0)
            {
                foreach (DataRow columna in tablaResultados.Rows)
                {
                    contrasenaFuncionario = Convert.ToString(columna["contraseña"]);

                    if (contrasenaFuncionario == contrasena) { esValido = true; }
                }
            }
            return esValido;
        }

        public Tuple<byte[], string> ObtenerFoto(string correo)
        {
            string nombreArchivo = "foto", tipoArchivo = "tipoArchivoFoto";
            string consulta = "SELECT " + nombreArchivo + ", "+ tipoArchivo + " FROM Funcionario WHERE correoPK = @correo";
            
            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@correo", correo }
            };

            return ObtenerArchivo(consulta, valoresParametros, nombreArchivo, tipoArchivo);
        }
        
    }
}