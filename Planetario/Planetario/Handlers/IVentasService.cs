using Planetario.Models;
using System.Collections.Generic;

namespace Planetario.Handlers
{
    public interface IVentasService
    {
        List<ProductoModel> ObtenerTodosLosProductos();

        bool InsertarProducto(ProductoModel producto);

        List<EntradaModel> ObtenerTodasLasEntradasDelCarrito(string correoUsuario);

        List<ProductoModel> ObtenerTodosLosProductosDelCarrito(string correoUsuario);

        bool EliminarDelCarrito(string correoUsuario, int idComprable);

        bool DisminiuirLaCantidadDelElementoDelCarrito(string correoUsuario, int idComprable);

        bool AumentarLaCantidadDelElementoDelCarrito(string correoUsuario, int idComprable);

        bool AgregarAlCarrito(int productID, int cantidad);

        double ObtenerPrecioTotalDeProductosDelCarrito(string correoUsuario);

        double ObtenerPrecioTotalDeEntradasDelCarrito(string correoUsuario);
    }
}