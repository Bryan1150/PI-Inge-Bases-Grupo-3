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
        public bool InsertarActividad(EventoModel evento)
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

    }
}