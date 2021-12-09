using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Planetario.Models;

namespace Planetario.Interfaces
{
    public interface ProductosInterfaz
    {
        List<ProductoModel> ObtenerProductosFiltrados(double precioMin, double precioMax, string categoria, string busqueda, string orden);
    }
}