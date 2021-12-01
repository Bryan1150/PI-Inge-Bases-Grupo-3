﻿using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;


namespace Planetario.Handlers
{
    public class VentasHandler : BaseDatosHandler, IVentasService
    {
        ArchivosHandler manejadorDeImagen = new ArchivosHandler();

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
                    FechaUltimaVenta = Convert.ToString(columna["fechaUltimaVenta"])
                });
            }
            return productos;
        }

        private List<ProductoModel> ObtenerProductos(string consulta)
        {
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<ProductoModel> productos = ConvertirTablaALista(tabla);
            return productos;
        }

        public List<ProductoModel> ObtenerTodosLosProductos()
        {
            string consulta = "SELECT idComprablePK, nombre, precio, cantidadDisponible, cantidadRebastecer, tamano, categoria, descripcion, fechaIngreso, fechaUltimaVenta " +
                              "FROM Producto JOIN Comprable " +
                              "ON idComprablePK = idComprableFK" +
                              "WHERE cantidadDisponible > 0";
            return (ObtenerProductos(consulta));
        }

        public bool InsertarProducto(ProductoModel producto)
        {
            string consultaTablaComprable = "INSERT INTO Comprable (nombre, precio, cantidadDisponible) " +
                                            "VALUES (@nombre, @precio, @cantidadDisponible);";

            string consultaTablaProducto = "DECLARE @identity int=IDENT_CURRENT('Comprable');" +
                                           "INSERT INTO Producto (idComprableFK, cantidadRebastecer, tamano, categoria, descripcion, fechaIngreso, fechaUltimaVenta, fotoArchivo, fotoTipo, cantidadVendidos) " +
                                           "VALUES ( @identity, @cantidadRebastecer, @tamano, @categoria, @descripcion, @fechaIngreso, NULL, @fotoArchivo, @fotoTipo, 0 ); ";

            Dictionary<string, object> parametrosComprable = new Dictionary<string, object> {
                {"@nombre", producto.Nombre },
                {"@precio", producto.Precio },
                {"@cantidadDisponible", producto.CantidadDisponible }
            };

            Dictionary<string, object> parametrosProducto = new Dictionary<string, object> {
                {"@cantidadRebastecer", producto.CantidadRebastecer },
                {"@tamano", producto.Tamano },
                {"@categoria", producto.Categoria },
                {"@descripcion", producto.Descripcion },
                {"@fechaIngreso", producto.FechaIngreso },
                {"@fotoTipo", producto.FotoArchivo.ContentType }
            };

            parametrosProducto.Add("@fotoArchivo", manejadorDeImagen.ConvertirArchivoABytes(producto.FotoArchivo));

            return (InsertarEnBaseDatos(consultaTablaComprable, parametrosComprable) && InsertarEnBaseDatos(consultaTablaProducto, parametrosProducto));
        }

        public List<ProductoModel> ObtenerTodasLasEntradasDelCarrito(string correoUsuario) 
        {
            string consulta = "SELECT * FROM Carrito C " +
                              "JOIN Entrada E " +
                              "ON C.idComprableFK = E.idComprableFK ;";
            return (ObtenerProductos(consulta));
        }

        public List<ProductoModel> ObtenerTodosLosProductosDelCarrito(string correoUsuario) 
        {
            string consulta = "SELECT * FROM Carrito C " +
                              "JOIN Producto P " +
                              "ON C.idComprableFK = P.idComprableFK ;";
            return (ObtenerProductos(consulta));
        }

        public bool EliminarDelCarrito(string correoUsuario, int idComprable)
        {
            string consulta = "DELETE FROM Carrito " + 
                              "WHERE idComprableFK = @idComprable " + 
                              "AND correoPersonaFK = @correoPersona ;";
            Dictionary<string, object> parametrosProducto = new Dictionary<string, object> {
                {"@idComprable"   , correoUsuario },
                {"@correoPersona" , idComprable },
            };

            return EliminarEnBaseDatos(consulta, parametrosProducto);
        }

        public bool DisminiuirLaCantidadDelElementoDelCarrito(string correoUsuario, int idComprable) {
            string consulta = "UPDATE Carrito " +
                              "SET cantidadProductos = cantidadProductos - 1 " +
                              "WHERE idComprableFK = @idComprable AND correoPersonaFK = @correoPersona ;";
            
            Dictionary<string, object> parametrosProducto = new Dictionary<string, object> {
                {"@idComprable"   , correoUsuario },
                {"@correoPersona" , idComprable },
            };

            return ActualizarEnBaseDatos(consulta, parametrosProducto);
        }

        public bool AumentarLaCantidadDelElementoDelCarrito(string correoUsuario, int idComprable) {
            string consulta = "UPDATE Carrito" +
            " SET cantidadProductos = cantidadProductos + 1" +
            " WHERE idComprableFK = @idComprable AND correoPersonaFK = @correoPersona ;";
            
            Dictionary<string, object> parametrosProducto = new Dictionary<string, object> {
                {"@idComprable"   , correoUsuario },
                {"@correoPersona" , idComprable },
            };

            return ActualizarEnBaseDatos(consulta, parametrosProducto);
        }

        public bool AgregarAlCarrito(int productID, int cantidad)
        {
            string consulta = "INSERT INTO Carrito (idComprableFK, correoPersonaFK, cantidadProductos) " + 
            "VALUES (@id, @correo, @cantidad)";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object> {
                {"@id", productID },
                {"@correo", HttpContext.Current.User.Identity.Name},
                {"@cantidad", cantidad}
            };

            return (InsertarEnBaseDatos(consulta, valoresParametros));
        }

        public double ObtenerPrecioTotalDeProductosDelCarrito(string correoUsuario)
        {
            string consulta = "SELECT SUM(CO.precio * C.cantidadProductos) AS 'Total' " +
                              "FROM Carrito C JOIN Comprable CO " +
                              "ON C.idComprableFK = CO.idComprablePK " +
                              "JOIN Producto P " +
                              "ON CO.idComprablePK = P.idComprableFK " +
                              "WHERE C.correoPersonaFK = '" + correoUsuario + "';";

            double total = 0;
            DataTable tabla = LeerBaseDeDatos(consulta);
            total = ConvertirTablaADouble(tabla);

            return total;
        }

        public double ObtenerPrecioTotalDeEntradasDelCarrito(string correoUsuario)
        {
            string consulta = "SELECT SUM(CO.precio * C.cantidadProductos) AS 'Total' " +
                              "FROM Carrito C JOIN Comprable CO " +
                              "ON C.idComprableFK = CO.idComprablePK " +
                              "JOIN Entrada E " +
                              "ON CO.idComprablePK = E.idComprableFK " +
                              "WHERE C.correoPersonaFK = '" + correoUsuario + "';";

            double total = 0;
            DataTable tabla = LeerBaseDeDatos(consulta);
            total = ConvertirTablaADouble(tabla);

            return total;
        }

        private double ConvertirTablaADouble(DataTable tabla)
        {
            double total = 0;
            foreach (DataRow columna in tabla.Rows)
            {
                total = Convert.ToDouble(columna["Total"]);
            };

            return total;
        }

    }
}