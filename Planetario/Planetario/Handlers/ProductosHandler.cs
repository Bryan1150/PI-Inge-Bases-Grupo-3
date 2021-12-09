using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Planetario.Interfaces;

namespace Planetario.Handlers
{
    public class ProductosHandler: BaseDatosHandler, ProductosInterfaz
    {
        private List<ProductoModel> ConvertirTablaProductoALista(DataTable tabla)
        {
            List<ProductoModel> productos = new List<ProductoModel>();
            foreach (DataRow columna in tabla.Rows)
            {
                productos.Add(
                new ProductoModel
                {
                    Id = Convert.ToInt32(columna["idComprablePK"]),
                    Nombre = Convert.ToString(columna["nombre"]),
                    Precio = Convert.ToDouble(columna["precio"]),
                    CantidadDisponible = Convert.ToInt32(columna["cantidadDisponible"]),
                    CantidadRebastecer = Convert.ToInt32(columna["cantidadRebastecer"]),
                    Tamano = Convert.ToString(columna["tamano"]),
                    Categoria = Convert.ToString(columna["categoria"]),
                    Descripcion = Convert.ToString(columna["descripcion"]),
                    FechaIngreso = Convert.ToString(columna["fechaIngreso"]),
                    FechaUltimaVenta = Convert.ToString(columna["fechaUltimaVenta"])
                });
            }
            return productos;
        }

        private List<ProductoModel> ObtenerProductos(string consulta)
        {
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<ProductoModel> productos = ConvertirTablaProductoALista(tabla);
            return productos;
        }

        public List<ProductoModel> ObtenerProductosFiltrados(double precioMin, double precioMax, string categoria, string busqueda, string orden)
        {

            string consulta = "SELECT * FROM Producto P JOIN Comprable C ON P.idComprableFK = C.idComprablePK " +
            "WHERE Precio >= " + precioMin.ToString() + " AND Precio <= " + precioMax.ToString() + " ";
            if (categoria != "" && categoria != null)
            {
                consulta += "AND P.categoria = '" + categoria + "' ";
            }
            if (busqueda != "" && busqueda != null)
            {
                consulta += "AND nombre LIKE '%" + busqueda + "%' ";
            }
            consulta += "ORDER BY " + orden + ";";

            return ObtenerProductos(consulta);
        }
    }
}