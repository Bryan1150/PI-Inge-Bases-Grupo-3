using System;
using System.Collections.Generic;
using System.Data;
using Planetario.Models;

namespace Planetario.Handlers
{
    public class MembresiasHandler : BaseDatosHandler
    {
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