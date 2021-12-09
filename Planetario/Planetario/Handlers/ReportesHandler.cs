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
    public class ReportesHandler : BaseDatosHandler, IReportesService
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
                    Nombre = Convert.ToString(columna["nombre"]),
                    Precio = Convert.ToDouble(columna["precio"]),
                    FechaIngreso = Convert.ToString(columna["fechaIngreso"]),
                    FechaUltimaVenta = Convert.ToString(columna["fechaUltimaVenta"]),
                    CantidadVendidos = Convert.ToInt32(columna["cantidadVendidos"])
                });
            }
            return productos;
        }

        public List<ProductoModel> ObtenerTodosLosProductosFiltradosPorRanking(int cantidadMostrar, string fechaInicio, string fechaFinal, string orden)
        {
            string consulta = "SELECT TOP " + cantidadMostrar + " nombre, precio, fechaIngreso, fechaUltimaVenta, cantidadVendidos " +
                              "FROM Producto P JOIN Comprable C " +
                              "ON idComprablePK = idComprableFK " +
                              "WHERE DATEDIFF(MINUTE, '" + fechaInicio + "', fechaUltimaVenta) >= 0 " +
                              "AND DATEDIFF(MINUTE, '" + fechaFinal + "', fechaUltimaVenta ) <= 0 " +
                              "ORDER BY cantidadVendidos " + orden + ";";
                              
            return (ObtenerProductosPorFiltro(consulta));
        }

        public List<ProductoModel> ObtenerTodosLosProductosFiltradosPorCategoria(string categoria, string fechaInicio, string fechaFinal)
        {
            string consulta = "SELECT nombre, precio, fechaIngreso, fechaUltimaVenta, cantidadVendidos " +
                              "FROM Producto P JOIN Comprable C " +
                              "ON idComprablePK = idComprableFK " +
                              "WHERE categoria = '" + categoria + "' " +
                              "AND DATEDIFF(MINUTE, '" + fechaInicio + "', fechaUltimaVenta) >= 0 " + 
                              "AND DATEDIFF(MINUTE, '" + fechaFinal + "', fechaUltimaVenta ) <= 0 " +
                              "ORDER by nombre ";

            return (ObtenerProductosPorFiltro(consulta));
        }

        public List<string> ObtenerTodasLasCategorias()
        {
            string consulta = "SELECT DISTINCT categoria  " +
                              "FROM Producto P JOIN Comprable C " +
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

        public string ConsultaPorCategoriasPersonaExtranjeras(string categoria)
        {
            string consulta = "SELECT SUM(F.cantidadComprada) as 'cantidad', Pe.pais, C.nombre, C.precio" +
                              "FROM Producto Pr JOIN Comprable C " +
                              "ON C.idComprablePK = Pr.idComprableFK " +
                              "JOIN Factura F" +
                              "ON F.idComprableFk = C.idComprablePK " +
                              "JOIN Persona Pe " +
                              "ON F.correoPersonaFK = Pe.correoPersonaPK " +
                              "WHERE Pr.categoria = '" + categoria + "' " +
                              "AND Pe.pais != 'Costa Rica'" +
                              "GROUP BY Pe.Pais, C.nombre, C.precio";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);


            string datosJson = "[";
            foreach (DataRow fila in tablaResultados.Rows)
            {
                datosJson += "{" +
                    "Nombre:" + Convert.ToString(fila["nombre"]) + "," +
                    "Pais:" + Convert.ToString(fila["pais"]) + "," +
                    "Precio:" + Convert.ToString(fila["precio"]) + "," +
                    "Cantidad:" + Convert.ToString(fila["cantidad"]) + "," +
                    "Ingresos:" + (Convert.ToInt32(fila["precio"]) * Convert.ToInt32(fila["cantidad"])) +
                    "},";
            }
            datosJson.Remove(datosJson.Length - 1);
            datosJson += "]";
            return datosJson;
        }

        public string ConsultaPorCategoriaProductoGeneroEdad(string categoria, string genero, string publico)
        {
            string consulta = "SELECT SUM(F.cantidadComprada) as 'cantidad', C.nombre, C.precio" +
                              "FROM Producto Pr JOIN Comprable C " +
                              "ON C.idComprablePK = Pr.idComprableFK " +
                              "JOIN Factura F" +
                              "ON F.idComprableFk = C.idComprablePK " +
                              "JOIN Persona Pe " +
                              "ON F.correoPersonaFK = Pe.correoPersonaPK " +
                              "WHERE Pr.categoria = '" + categoria + "' " +
                              "AND Pe.genero = '" + genero + "' " +
                              "AND procx(fechaNacimiento) = '" + publico + "';";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            string datosJson = "[";
            foreach (DataRow fila in tablaResultados.Rows)
            {
                datosJson += "{" +
                    "Nombre:" + Convert.ToString(fila["nombre"]) + "," +
                    "Cantidad:" + Convert.ToString(fila["cantidad"]) + "," +
                    "Precio:" + Convert.ToString(fila["precio"]) + "," +
                    "Ingresos:" + (Convert.ToInt32(fila["precio"]) * Convert.ToInt32(fila["cantidad"])) + "," +
                    "},";
            }
            datosJson.Remove(datosJson.Length - 1);
            datosJson += "]";
            return datosJson;
        }

        public string ConsultaProductosCompradosJuntos()
        {
            string consulta = "WITH TABLA_PRODUCTOS_COMPRADOS_JUNTOS AS " +
                "( SELECT F1.idComprableFK AS 'Producto', F2.idComprableFK AS 'CompradoCon' " +
                "FROM Factura F1 INNER JOIN Factura F2 ON F1.idComprableFK = F2.idComprableFK ) " +
                "SELECT " +
                "C1.nombre AS 'Producto', " +
                "C2.nombre AS 'ProductoCompradoCon', " +
                "COUNT(*) AS 'VecesCompradosJuntos' " +
                "FROM TABLA_PRODUCTOS_COMPRADOS_JUNTOS PC " +
                "JOIN Comprable C1 ON PC.Producto = C1.idComprablePK " +
                "JOIN Comprable C2 ON PC.Producto = C2.idComprablePK " +
                "GROUP BY C1.nombre, C2.nombre";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            string datosJson = "[";
            foreach(DataRow fila in tablaResultados.Rows)
            {
                datosJson += "{" +
                    "Producto:" + Convert.ToString(fila["Producto"]) + "," +
                    "CompradoCon:" + Convert.ToString(fila["ProductoCompradoCon"]) + "," +
                    "Cantidad de Veces:" + Convert.ToString(fila["VecesCompradosJuntos"]) + "," +
                    "},";
            }
            datosJson.Remove(datosJson.Length - 1);
            datosJson += "]";
            return datosJson;
        }
    }
}