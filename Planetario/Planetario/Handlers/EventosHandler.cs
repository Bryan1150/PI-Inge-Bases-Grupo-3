using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Planetario.Models;

namespace Planetario.Handlers
{
    public class EventosHandler: BaseDatosHandler
    {
        public bool InsertarEvento(EventoModel evento)
        {
            bool exito;
            string Consulta = "INSERT INTO Eventos ( titulo, fecha, descripcion ) "
                + "VALUES ( @titulo, @fecha, @descripcion );";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@titulo", evento.Titulo },
                {"@fecha", evento.Fecha },
                {"@descripcion", evento.Descripcion }
            };

            exito = InsertarEnBaseDatos(Consulta, valoresParametros);

            return exito;
        }

        public List<EventoModel> ObtenerTodosEventos()
        {
            List<EventoModel> eventos = new List<EventoModel>();
            string Consulta = "SELECT * FROM Eventos";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                eventos.Add(
                    new EventoModel
                    {
                        Titulo = Convert.ToString(columna["@diaSemana"]),
                        Fecha = Convert.ToString(columna["@propuestoPorFK"]),
                        Descripcion = Convert.ToString(columna["@publicoDirigidoActividad"])
                    });
            }
            return eventos;
        }

        public EventoModel ObtenerUnEvento(string titulo)
        {
            EventoModel evento = new EventoModel { Titulo = "pureba", Fecha = "2021-11-01", Descripcion = "p2kk" };
            string Consulta = "SELECT * FROM Eventos WHERE titulo = " + titulo + ";";
            DataTable tablaResultado = LeerBaseDeDatos(Consulta);
            if (tablaResultado.Rows.Count >= 1)
            {
                evento = new EventoModel
                {
                    Titulo = Convert.ToString(tablaResultado.Rows[0]["@diaSemana"]),
                    Fecha = Convert.ToString(tablaResultado.Rows[0]["@propuestoPorFK"]),
                    Descripcion = Convert.ToString(tablaResultado.Rows[0]["@publicoDirigidoActividad"])
                };
            }
            return evento;
        }
    }
}