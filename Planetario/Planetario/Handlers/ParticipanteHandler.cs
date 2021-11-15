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

        public bool AlmacenarParticipacion(string correo, string nombreActividad, double precio)
        {
            string consulta = "INSERT INTO Factura (pagoTotal, correoParticipanteFK, nombreActividadFK) VALUES (@pagoTotal, @correoParticipanteFK, @nombreActividadFK)";
            Dictionary<string, object> valoresParametros = new Dictionary<string, object>()
            {
                { "@nombreActividadFK", nombreActividad },
                { "@correoParticipanteFK", correo },
                { "@pagoTotal", precio }
            };

            bool exito = InsertarEnBaseDatos(consulta, valoresParametros);

            return exito;
        }
        public string AppendRestante(string toAppend, int comprobacion)
        {
            if (comprobacion < 10)
            {
                toAppend = "0" + toAppend;
            }
            return toAppend;
        }
        public string TranslateFecha(string fecha)
        {
            DateTime fechaDateTime = DateTime.Parse(fecha);
            string year = AppendRestante(fechaDateTime.Year.ToString(), fechaDateTime.Year);
            string month = AppendRestante(fechaDateTime.Month.ToString(), fechaDateTime.Month);
            string day = AppendRestante(fechaDateTime.Day.ToString(), fechaDateTime.Day);

            string resultado = year + '-' + month + '-' + day;
            return resultado;
        }
        public ParticipanteModel GetParticipante(string correo)
        {
            string consulta = "SELECT * FROM Participante WHERE correoParticipantePK = '" + correo + "';";
            DataTable resultado = LeerBaseDeDatos(consulta);
            return (new ParticipanteModel()
            {
                Correo = Convert.ToString(resultado.Rows[0]["correoParticipantePK"]),
                Nombre = Convert.ToString(resultado.Rows[0]["nombre"]),
                Apellido1 = Convert.ToString(resultado.Rows[0]["apellido1"]),
                Apellido2 = Convert.ToString(resultado.Rows[0]["apellido2"]),
                Genero = Convert.ToString(resultado.Rows[0]["genero"]),
                Pais = Convert.ToString(resultado.Rows[0]["pais"]),
                FechaNacimiento = TranslateFecha(Convert.ToString(resultado.Rows[0]["fechaNacimiento"])),
                NivelEducativo = Convert.ToString(resultado.Rows[0]["nivelEducativo"])
            });
        }
    }
}