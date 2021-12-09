using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Planetario.Interfaces;


namespace Planetario.Handlers
{
    public class DescuentosHandler: BaseDatosHandler, DescuentosInterfaz
    {
        private List<DescuentoModel> ConvertirTablaAListaDescuento(DataTable tabla)
        {
            List<DescuentoModel> productos = new List<DescuentoModel>();
            foreach (DataRow columna in tabla.Rows)
            {
                productos.Add(
                new DescuentoModel
                {
                    Codigo = Convert.ToString(columna["codigoDescuentoPK"]),
                    Descuento = Convert.ToInt32(columna["porcentajeDescuent"]),
                    Membresia = Convert.ToString(columna["membresia"]),
                });
            }
            return productos;
        }

        public List<DescuentoModel> ObtenerTodosDescuentos(string codigo)
        {
            string consulta = "SELECT FROM Descuento WHERE codigoDescuentoPK";
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<DescuentoModel> descuento = ConvertirTablaAListaDescuento(tabla);
            return descuento;
        }

        public DescuentoModel ObtenerDescuento(string codigo)
        {
            string consulta = "SELECT FROM Descuento WHERE codigoDescuentoPK = '" + codigo + "';";
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<DescuentoModel> descuento = ConvertirTablaAListaDescuento(tabla);
            return descuento[0];
        }

        public bool InsertarDescuento(DescuentoModel descuento)
        {
            string consulta = "INSERT INTO Descuento (codigoDescuentoPK, porcentajeDescuento, membresia) " +
                                "VALUES (@codigo, @porcentaje, @membresia)";
            Dictionary<string, object> parametrosProducto = new Dictionary<string, object> {
                {"@codigo"   , descuento.Codigo },
                {"@porcentaje"   , descuento.Descuento },
                {"@membresia"   , descuento.Membresia }
            };
            return (InsertarEnBaseDatos(consulta, parametrosProducto));
        }

        public bool EliminarDescuento(string codigo)
        {
            string consulta = "DELETE FROM Descuento WHERE codigoDescuentoPK = '@codigo';";
            Dictionary<string, object> parametrosProducto = new Dictionary<string, object> {
                {"@codigo"   , codigo }
            };
            return EliminarEnBaseDatos(consulta, parametrosProducto);
        }
    }
}