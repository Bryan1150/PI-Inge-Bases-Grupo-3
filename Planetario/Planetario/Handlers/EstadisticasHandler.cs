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
    public class EstadisticasHandler : BaseDatosHandler
    {

        public int obtenerCantidadDeParticipantes(string diaSemana, string publicoMeta, string nivelComplejidad)
        {
            int cantidadTotal;

            string consulta = crearStringDeConsultaCantidad(diaSemana, publicoMeta, nivelComplejidad);

            DataTable tablaResultado = LeerBaseDeDatos(consulta);
            DataRow columna = tablaResultado.Rows[0];

            cantidadTotal = Convert.ToInt32(columna["Participantes"]);

            return cantidadTotal;
        }

        public string crearStringDeConsultaCantidad(string diaSemana, string publicoMeta, string nivelComplejidad)
        {
            string consulta = "SELECT COUNT(*) as 'Participantes' " +
                              "FROM ParticipaEn P JOIN Actividad A " +
                              "ON A.nombreActividadPK = P.nombreActividadFK ";

            if (diaSemana != "")
            {
                consulta += " WHERE diaSemana = '" + diaSemana + "' ";
            }


            if (publicoMeta != "")
            {
                consulta += " AND publicoDirigidoActividad = '" + publicoMeta + "' ";
            }

            if (nivelComplejidad != "")
            {
                consulta += " AND complejidad = '" + nivelComplejidad + "' ";
            }

            return consulta;
        }
    }
}