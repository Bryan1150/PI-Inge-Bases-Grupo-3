using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Planetario.Models;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data.SqlTypes;
using System.Web;

namespace Planetario.Handlers
{
    public class FacturasHandler: BaseDatosHandler
    {
        public List<FacturaModel> ConvertirTablaALista(DataTable tabla)
        {
            List<FacturaModel> facturas = new List<FacturaModel>();
            foreach (DataRow columna in tabla.Rows)
            {
                facturas.Add(
                    new FacturaModel
                    {
                        id = Convert.ToInt32(columna["idFacturaPK"]),
                        fecha = Convert.ToString(columna["fechaCompra"]),
                        pago = Convert.ToDouble(columna["pagoTotal"]),
                        correoCliente = Convert.ToString(columna["correoParticipanteFK"]),
                        actividad = Convert.ToString(columna["nombreActividadFK"]),
                    });
            }
            return facturas;
        }

        public List<FacturaModel> ObtenerFacturas(string consulta)
        {
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<FacturaModel> lista = ConvertirTablaALista(tabla);
            return lista;
        }

        public List<FacturaModel> ObtenerTodasLasFacturas()
        {
            string consulta = "SELECT * FROM Factura;";
            return (ObtenerFacturas(consulta));
        }

        public List<FacturaModel> ObtenerFacturasDeActividad(string nombreActividad)
        {
            string consulta = "EXEC USP_ObtenerTodosPagos '" + nombreActividad + "';";
            return (ObtenerFacturas(consulta));
        }

        public FacturaModel ObtenerFactura(int id)
        {
            string consulta = "SELECT * FROM Factura WHERE idFacturaPK ='" + id.ToString() + "';";
            return (ObtenerFacturas(consulta)[0]);
        }
    }
}