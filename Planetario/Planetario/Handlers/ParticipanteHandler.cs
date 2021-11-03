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
            string columnas = "(  correoParticipantePKm, nombre, apellido1, apellido2, genero, pais, fechaNacimiento, nivelEducativo )";
            string valores = "( @correoParticipantePKm, @nombre, @apellido1, @apellido2, @genero, @pais, @fechaNacimiento, @nivelEducativo );";
            consulta += columnas + " VALUES " + valores;

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>()
            {
                { "@correoParticipantePKm", participante.Correo },
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
            string consulta = "SELECT 1 AS 'Inscrito' FROM Participante WHERE correoParticipantePK = " + correo + ";";
            DataTable resultado = LeerBaseDeDatos(consulta);

            return (Convert.ToInt32(resultado.Rows[0]["Inscrito"]) == 1);
        }

        public bool AlmacenarParticipacion(string correo, string nombreActividad)
        {
            string consulta = "INSERT INTO ParticipaEN VALUES (@nombreActividadFK, @correoParticipanteFK)";
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