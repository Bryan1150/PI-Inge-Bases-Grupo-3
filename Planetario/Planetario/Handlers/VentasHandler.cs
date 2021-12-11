using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Planetario.Interfaces;

namespace Planetario.Handlers
{
    public class VentasHandler : BaseDatosHandler, VentasInterfaz
    {

        private List<ComprableModel> ConvertirTablaComprablesALista(DataTable tabla)
        {
            List<ComprableModel> comprables = new List<ComprableModel>();
            foreach (DataRow columna in tabla.Rows)
            {
                comprables.Add(
                new ProductoModel
                {
                    Id = Convert.ToInt32(columna["idComprablePK"]),
                    Nombre = Convert.ToString(columna["nombre"]),
                    Precio = Convert.ToDouble(columna["precio"]),
                    CantidadDisponible = Convert.ToInt32(columna["cantidadDisponible"]),
                    CantidadCarrito = Convert.ToInt32(columna["cantidadProductos"]),
                });
            }
            return comprables;
        }

        private List<ComprableModel> ObtenerComprables(string consulta)
        {
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<ComprableModel> comprables = ConvertirTablaComprablesALista(tabla);
            return comprables;
        }

        public List<ComprableModel> ObtenerTodasLasEntradasDelCarrito(string correoUsuario) 
        {
            string consulta = "SELECT * FROM Carrito C " +
                              "JOIN Entrada E " +
                              "ON C.idComprableFK = E.idComprableFK " +
                              "JOIN Comprable CO " +
                              "ON E.idComprableFK = CO.idComprablePK " + 
                              "WHERE correoPersonaFK = '" + correoUsuario + "' ";
            return (ObtenerComprables(consulta));
        }

        public List<ComprableModel> ObtenerTodosLosProductosDelCarrito(string correoUsuario) 
        {
            string consulta = "SELECT * FROM Carrito C " +
                              "JOIN Producto P " +
                              "ON C.idComprableFK = P.idComprableFK " +
                              "JOIN Comprable CO " +
                              "ON P.idComprableFK = CO.idComprablePK " +
                              "WHERE correoPersonaFK = '" + correoUsuario + "' ";
            return (ObtenerComprables(consulta));
        }

        public bool EliminarDelCarrito(string correoUsuario, int idComprable)
        {
            string consulta = "DELETE FROM Carrito " + 
                              "WHERE idComprableFK = @idComprable " + 
                              "AND correoPersonaFK = @correoPersona ;";

            Dictionary<string, object> parametrosProducto = CrearDiccionarioDiccionarioCarrito(correoUsuario, idComprable);
            return EliminarEnBaseDatos(consulta, parametrosProducto);
        }

        public bool DisminiuirLaCantidadDelElementoDelCarrito(string correoUsuario, int idComprable) {
            string consulta = "UPDATE Carrito " +
                              "SET cantidadProductos = cantidadProductos - 1 " +
                              "WHERE idComprableFK = @idComprable AND correoPersonaFK = @correoPersona ;";

            Dictionary<string, object> parametrosProducto = CrearDiccionarioDiccionarioCarrito(correoUsuario, idComprable);
            return ActualizarEnBaseDatos(consulta, parametrosProducto);
        }

        public bool AumentarLaCantidadDelElementoDelCarrito(string correoUsuario, int idComprable) {
            string consulta = "UPDATE Carrito" +
            " SET cantidadProductos = cantidadProductos + 1" +
            " WHERE idComprableFK = @idComprable AND correoPersonaFK = @correoPersona ;";

            Dictionary<string, object> parametrosProducto = CrearDiccionarioDiccionarioCarrito(correoUsuario, idComprable);

            return ActualizarEnBaseDatos(consulta, parametrosProducto);
        }

        private Dictionary<string, object> CrearDiccionarioDiccionarioCarrito(string correoUsuario, int idComprable)
        {
            Dictionary<string, object> parametrosProducto = new Dictionary<string, object> {
                {"@idComprable"   , idComprable },
                {"@correoPersona" , correoUsuario },
            };
            return parametrosProducto;
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

            DataTable resultadoConsulta = LeerBaseDeDatos(consulta);
            double total = Convert.ToDouble(resultadoConsulta.Rows[0]["Total"]);
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

            DataTable resultadoConsulta = LeerBaseDeDatos(consulta);
            double total = Convert.ToDouble(resultadoConsulta.Rows[0]["Total"]);
            return total;
        }

        public int ObtenerCantidadDeEntradasDelCarrito(string correoUsuario)
        {
            string consulta = "SELECT COUNT(*) AS 'Cantidad' FROM Carrito C " +
                              "JOIN Entrada E " +
                              "ON C.idComprableFK = E.idComprableFK " +
                              "JOIN Comprable CO " +
                              "ON E.idComprableFK = CO.idComprablePK " +
                              "WHERE correoPersonaFK = '" + correoUsuario + "' ";

            DataTable resultadoConsulta = LeerBaseDeDatos(consulta);
            int total = Convert.ToInt32(resultadoConsulta.Rows[0]["Cantidad"]);
            return total;
        }

        public int ObtenerCantidadDeProductosDelCarrito(string correoUsuario)
        {
            string consulta = "SELECT COUNT(*) AS 'Cantidad' FROM Carrito C " +
                              "JOIN Producto P " +
                              "ON C.idComprableFK = P.idComprableFK " +
                              "JOIN Comprable CO " +
                              "ON P.idComprableFK = CO.idComprablePK " +
                              "WHERE correoPersonaFK = '" + correoUsuario + "' ";

            DataTable resultadoConsulta = LeerBaseDeDatos(consulta);
            int total = Convert.ToInt32(resultadoConsulta.Rows[0]["Cantidad"]);
            return total;
        }
    }
}