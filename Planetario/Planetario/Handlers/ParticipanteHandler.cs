using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Planetario.Handlers
{
    public class ParticipanteHandler : BaseDatosHandler
    {
        public bool AlmacenarParticipante(ParticipanteModel participante)
        {
            string consulta = "INSERT INTO Participante ";
            string columnas = "(  correoParticipantePK, nombre, apellido1, apellido2, genero, pais, fechaNacimiento, nivelEducativo )";
            string valores = "( @correoParticipantePK, @nombre, @apellido1, @apellido2, @genero, @pais, @fechaNacimiento, @nivelEducativo );";
            consulta += columnas + " VALUES " + valores;

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>()
            {
                { "@correoParticipantePK", participante.Correo },
                { "@nombre", participante.Nombre },
                { "@apellido1", participante.Apellido1 },
                { "@apellido2", participante.Apellido2 },
                { "@genero", participante.Genero },
                { "@pais", participante.Pais },
                { "@fechaNacimiento", participante.FechaNacimiento },
                { "@nivelEducativo" , participante.NivelEducativo }
            };

            bool exito = InsertarEnBaseDatos(consulta, valoresParametros);

            return exito;
        }

        public bool ParticipanteEstaAlmacenado(string correo)
        {
            string consulta = "SELECT 1 AS 'Inscrito' FROM Participante WHERE correoParticipantePK = '" + correo + "';";
            DataTable resultado = LeerBaseDeDatos(consulta);
            try
            {
                return (Convert.ToInt32(resultado.Rows[0]["Inscrito"]) == 1);
            }
            catch
            {
                return (false);
            }
        }

        public bool AlmacenarParticipacion(string correo, string nombreActividad)
        {
            string consulta = "INSERT INTO Factura (pagoTotal, correoParticipanteFK, nombreActividadFK) VALUES (20000.00, @correoParticipanteFK, @nombreActividadFK)";
            Dictionary<string, object> valoresParametros = new Dictionary<string, object>()
            {
                { "@nombreActividadFK", nombreActividad },
                { "@correoParticipanteFK", correo }
            };

            bool exito = InsertarEnBaseDatos(consulta, valoresParametros);

            return exito;
        }

    }
}