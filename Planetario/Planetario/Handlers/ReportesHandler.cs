using System;
using System.Collections.Generic;
using System.Data;
using Planetario.Models;

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

        public List<Object> ObtenerTodosLosProductosFiltradosPorRanking(int cantidadMostrar, string fechaInicio, string fechaFinal, string orden)
        {
            string consulta = "SELECT TOP " + cantidadMostrar + " nombre, precio, fechaIngreso, fechaUltimaVenta, cantidadVendidos " +
                              "FROM Producto P JOIN Comprable C " +
                              "ON idComprablePK = idComprableFK " +
                              "WHERE DATEDIFF(MINUTE, '" + fechaInicio + "', fechaUltimaVenta) >= 0 " +
                              "AND DATEDIFF(MINUTE, '" + fechaFinal + "', fechaUltimaVenta ) <= 0 " +
                              "ORDER BY cantidadVendidos " + orden + ";";

            List<Object> info = new List<Object>();
            DataTable tabla = LeerBaseDeDatos(consulta);
            foreach (DataRow columna in tabla.Rows)
            {
                info.Add(new
                {
                    Nombre = Convert.ToString(columna["nombre"]),
                    Precio = Convert.ToDouble(columna["precio"]),
                    FechaIngreso = Convert.ToString(columna["fechaIngreso"]),
                    FechaUltimaVenta = Convert.ToString(columna["fechaUltimaVenta"]),
                    CantidadVendidos = Convert.ToInt32(columna["cantidadVendidos"])
                });
            }
            return info;
        }

        public List<string> ObtenerTodosLosProductosFiltradosPorCategoriaFechasVentas(string nombre, string fechaInicio, string fechaFinal)
        {
            string consulta = "SELECT F.fechaCompra " +
                              "FROM Producto P JOIN Comprable C " +
                              "ON C.idComprablePK = P.idComprableFK " +
                              "JOIN Factura F ON C.idComprablePK = F.idComprableFK " +
                              "WHERE C.nombre = '" + nombre + "' " +
                              "AND DATEDIFF(MINUTE, '" + fechaInicio + "', fechaUltimaVenta) >= 0 " + 
                              "AND DATEDIFF(MINUTE, '" + fechaFinal + "', fechaUltimaVenta ) <= 0 " +
                              "ORDER by F.fechaCompra ASC";

            DataTable tabla = LeerBaseDeDatos(consulta);
            List<string> fechas = new List<string>();
            foreach (DataRow columna in tabla.Rows)
            {
                fechas.Add(Convert.ToString(columna["fechaCompra"]));
            }
            return fechas;
        }

        public List<int> ObtenerTodosLosProductosFiltradosPorCategoriaCantidadVentas(string nombre, string fechaInicio, string fechaFinal)
        {
            string consulta = "SELECT F.cantidadComprada " +
                              "FROM Producto P JOIN Comprable C " +
                              "ON C.idComprablePK = P.idComprableFK " +
                              "JOIN Factura F ON C.idComprablePK = F.idComprableFK " +
                              "WHERE C.nombre = '" + nombre + "' " +
                              "AND DATEDIFF(MINUTE, '" + fechaInicio + "', fechaUltimaVenta) >= 0 " +
                              "AND DATEDIFF(MINUTE, '" + fechaFinal + "', fechaUltimaVenta ) <= 0 " +
                              "ORDER by F.fechaCompra ASC";

            DataTable tabla = LeerBaseDeDatos(consulta);
            List<int> ventas = new List<int>();
            foreach (DataRow columna in tabla.Rows)
            {
                ventas.Add(Convert.ToInt32(columna["cantidadComprada"]));
            }
            return ventas;
        }

        public List<string> ObtenerTodasLasCategorias()
        {
            string consulta = "SELECT DISTINCT categoria " +
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

        public List<string> ObtenerTodosLosProductos()
        {
            string consulta = "SELECT DISTINCT C.nombre " +
                              "FROM Producto P JOIN Comprable C " +
                              "ON idComprablePK = idComprableFK " +
                              "ORDER BY C.nombre ASC;";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<string> productos = new List<string>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                productos.Add(Convert.ToString(fila["nombre"]));
            }

            return productos;
        }

        public List<object> ConsultaPorCategoriasPersonaExtranjeras(string categoria)
        {
            string consulta = "SELECT SUM(F.cantidadComprada) as 'cantidad', Pe.pais, C.nombre, C.precio AS precio " +
                              "FROM Producto Pr JOIN Comprable C " +
                              "ON C.idComprablePK = Pr.idComprableFK " +
                              "JOIN Factura F " +
                              "ON F.idComprableFk = C.idComprablePK " +
                              "JOIN Persona Pe " +
                              "ON F.correoPersonaFK = Pe.correoPersonaPK " +
                              "WHERE Pr.categoria = '" + categoria + "' " +
                              "AND Pe.pais != 'Costa Rica' " +
                              "GROUP BY Pe.Pais, C.nombre, C.precio";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<object> info = new List<object>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                info.Add( new 
                { 
                    Nombre = Convert.ToString(fila["nombre"]),
                    Pais = Convert.ToString(fila["pais"]),
                    Precio = Convert.ToString(fila["precio"]),
                    Cantidad = Convert.ToString(fila["cantidad"]),
                    Ingresos = (Convert.ToInt32(fila["precio"]) * Convert.ToInt32(fila["cantidad"]))
                });
            }

            return info;
        }

        public List<object> ConsultaPorCategoriaProductoGeneroEdad(string categoria, string genero, string publico)
        {
            string consulta = "SELECT SUM(F.cantidadComprada) as 'cantidad', C.nombre, C.precio " +
                              "FROM Producto Pr JOIN Comprable C " +
                              "ON C.idComprablePK = Pr.idComprableFK " +
                              "JOIN Factura F " +
                              "ON F.idComprableFk = C.idComprablePK " +
                              "JOIN Persona Pe " +
                              "ON F.correoPersonaFK = Pe.correoPersonaPK " +
                              "WHERE Pr.categoria = '" + categoria + "' " +
                              "AND Pe.genero = '" + genero + "' " +
                              "AND dbo.UFN_CategoriaPorEdad(DATEDIFF(YEAR, Pe.fechaNacimiento, GETDATE())) = '" + publico + "' " +
                              "GROUP BY C.nombre, C.precio;";

            

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<object> info = new List<object>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                info.Add(new
                {
                    Nombre = Convert.ToString(fila["nombre"]),
                    Precio = Convert.ToString(fila["precio"]),
                    Cantidad = Convert.ToString(fila["cantidad"]),
                    Ingresos = (Convert.ToInt32(fila["precio"]) * Convert.ToInt32(fila["cantidad"]))
                });
            }

            return info;
        }

        public List<object> ConsultaProductosCompradosJuntos(string publico, string membresia)
        {
            string consulta = "WITH TABLA_PRODUCTOS_COMPRADOS_JUNTOS AS ( " +
                "SELECT F1.idComprableFK AS 'Producto', F2.idComprableFK AS 'CompradoCon', " +
                "P.membresia, dbo.UFN_CategoriaPorEdad(DATEDIFF(YEAR, P.fechaNacimiento, GETDATE())) AS 'Publico' " +
                "FROM Factura F1 " +
                "INNER JOIN Factura F2 " +
                "ON F1.correoPersonaFK = F2.correoPersonaFK " +
                "JOIN Persona P    ON F1.correoPersonaFK = P.correoPersonaPK) " +
                "SELECT C1.nombre AS 'Producto', C1.precio AS 'PrecioProducto', " +
                "C2.nombre AS 'ProductoCompradoCon', C2.precio AS 'PrecioCompradoCon', " +
                "COUNT(*) AS 'VecesCompradosJuntos',    PC.membresia AS 'MembresiaCliente', " +
                "PC.Publico AS 'PublicoCliente' " +
                "FROM TABLA_PRODUCTOS_COMPRADOS_JUNTOS PC " +
                "JOIN Comprable C1 ON PC.Producto = C1.idComprablePK " +
                "JOIN Comprable C2 ON PC.CompradoCon = C2.idComprablePK " +
                "WHERE NOT Producto = CompradoCon " +
                "AND PC.membresia = '" + membresia + "' " +
                "AND PC.Publico = '" + publico + "' " +
                "GROUP BY C1.nombre, C2.nombre, C1.precio, C2.precio, PC.membresia, PC.Publico";

            DataTable tablaResultados = LeerBaseDeDatos(consulta);
            List<object> info = new List<object>();

            foreach (DataRow fila in tablaResultados.Rows)
            {
                int vecesCompradosJuntos = Convert.ToInt32(fila["VecesCompradosJuntos"]);
                int precioProducto = Convert.ToInt32(fila["PrecioProducto"]);
                int precioCompradoCon = Convert.ToInt32(fila["PrecioCompradoCon"]);
                info.Add(new
                {
                    Producto = Convert.ToString(fila["Producto"]),
                    CompradoCon = Convert.ToString(fila["ProductoCompradoCon"]),
                    CantidadVeces = vecesCompradosJuntos,
                    Ingresos = (vecesCompradosJuntos * (precioProducto+precioCompradoCon))
                });
            }

            return info;
        }
    }
}