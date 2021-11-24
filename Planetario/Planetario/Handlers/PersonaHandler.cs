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
    }
}