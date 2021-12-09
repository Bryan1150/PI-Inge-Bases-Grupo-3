using System;
using System.Collections.Generic;
using System.Data;
using Planetario.Models;

namespace Planetario.Handlers
{
    public class PersonaHandler: BaseDatosHandler
    {
        public bool EsUsuarioValido(string correo, string contrasena)
        {
            bool esValido = false;
            string contrasenaFuncionario;

            string consulta = "SELECT [dbo].UFN_compararContrasenas('" + contrasena + "', contraseña) AS 'resultado' FROM Credenciales WHERE correoPersonaFK = '" + correo + "';";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);

            if (tablaResultados.Rows.Count > 0)
            {
                foreach (DataRow columna in tablaResultados.Rows)
                {
                    contrasenaFuncionario = Convert.ToString(columna["resultado"]);
                    if (contrasenaFuncionario == "correcta") { esValido = true; }
                }
            }
            return esValido;
        }

        public bool EsFuncionario(string correo)
        {
            FuncionariosHandler funcionariosHandler = new FuncionariosHandler();
            return (funcionariosHandler.EstaEnTabla(correo));
        }

        public string ObtenerTipoUsuario(string correo)
        {
            FuncionariosHandler funcionariosHandler = new FuncionariosHandler();
            string tipoUsuario;
            if (funcionariosHandler.EstaEnTabla(correo))
            {
                tipoUsuario = "Funcionario";
            }
            else
            {
                tipoUsuario = "Cliente";
            }
            return tipoUsuario;
        }

        public bool InsertarUsuario(PersonaModel persona)
        {
            string consultaTablaPersona = "INSERT INTO Persona ( correoPersonaPK, nombre, apellido1, apellido2, genero, pais, fechaNacimiento, membresia ) "
                + "VALUES ( @correo, @nombre, @apellido1, @apellido2, @genero, @pais, @nacimiento, @membresia );";

            string consultaTablaCliente = "INSERT INTO Cliente ( correoClienteFK, nivelEducativo ) "
                + "VALUES ( @correo, @nivelEducativo );";

            Dictionary<string, object> parametrosPersona = new Dictionary<string, object> {
                {"@correo", persona.correo },
                {"@nombre", persona.nombre },
                {"@apellido1", persona.apellido1 },
                {"@apellido2", persona.apellido2 },
                {"@genero", persona.genero },
                {"@pais", persona.pais },
                {"@nacimiento", persona.fechaNacimiento},
                {"@membresia", "Terrestre" }
            };

            Dictionary<string, object> parametrosCliente = new Dictionary<string, object>
            {
                {"@correo", persona.correo },
                {"@nivelEducativo", persona.nivelEducativo },
            };

            return (InsertarEnBaseDatos(consultaTablaPersona, parametrosPersona) && InsertarEnBaseDatos(consultaTablaCliente, parametrosCliente));
        }

        public string ObtenerMembresia(string correo) 
        {
            string consultaTablaPersona = "SELECT membresia " +
                                          "FROM Persona " +
                                          "WHERE correoPersonaPK = '" + correo + "' ";

            DataTable tabla = LeerBaseDeDatos(consultaTablaPersona);
            DataRow columna = tabla.Rows[0];
            string membresia = Convert.ToString(columna["membresia"]);

            return membresia;
        }

        public bool ActualizarMembresia(string correo, string membresia)
        {
            string consultaTablaPersona = "UPDATE Persona " +
                                          "SET membresia = '" + membresia + "', " +
                                          "compraMembresia = GETDATE() " +
                                          "WHERE correoPersonaPK = '" + correo + "' ";

            return (ActualizarEnBaseDatos(consultaTablaPersona, null));
        }
    }
}