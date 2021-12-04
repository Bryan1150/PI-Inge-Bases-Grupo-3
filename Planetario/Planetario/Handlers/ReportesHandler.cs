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
    public class ReportesHandler : BaseDatosHandler
    {
        private List<ProductoModel> ObtenerProductosPorFiltro(string consulta)
        {
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<ProductoModel> productos = ConvertirTablaALista(tabla);
            return productos;
        }

        private List<ProductoModel> ConvertirTablaALista(DataTable tabla)
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
                    FechaUltimaVenta = Convert.ToString(columna["fechaUltimaVenta"]),
                    CantidadVendidos = Convert.ToInt32(columna["cantidadVendidos"])
                });
            }
            return productos;
        }

        public List<ProductoModel> ObtenerTodosLosProductosFiltradosPorRanking(int cantidadMostrar, string fechaInicio, string fechaFinal, string orden)
        {
            string consulta = "SELECT TOP '" + cantidadMostrar + "' nombre, cantidadVendidos, categoria " +
                              "FROM Producto JOIN Comprable C " +
                              "ON idComprablePK = idComprableFK " +
                              "WHERE DATEDIFF(MINUTE, '" + fechaInicio + "', fechaUltimaVenta) >= 0 " +
                              "AND DATEDIFF(MINUTE, '" + fechaFinal + "', fechaUltimaVenta ) <= 0 " +
                              "ORDER BY cantidadVendidos " + orden + ";";
                              
            return (ObtenerProductosPorFiltro(consulta));
        }

        public List<ProductoModel> ObtenerTodosLosProductosFiltradosPorCategoria(string categoria, string fechaInicio, string fechaFinal)
        {
            string consulta = "SELECT nombre, cantidadVendidos " +
                              "FROM Producto JOIN Comprable C " +
                              "ON idComprablePK = idComprableFK " +
                              "WHERE categoria = '" + categoria + "' " +
                              "DATEDIFF(MINUTE, '" + fechaInicio + "', fechaUltimaVenta) >= 0 " + 
                              "AND DATEDIFF(MINUTE, '" + fechaFinal + "', fechaUltimaVenta ) <= 0 " +
                              "ORDER by nombre ";

            return (ObtenerProductosPorFiltro(consulta));
        }

        public List<string> ObtenerTodasLasCategorias()
        {
            string consulta = "SELECT DISTINCT categoria  " +
                              "FROM Producto JOIN Comprable C " +
                              "ON idComprablePK = idComprableFK " +
                              "ORDER BY categoria ASC;";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> categorias = new List<string>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                categorias.Add(Convert.ToString(fila["categoria"]));
            }

            return categorias;
        }
    }
}