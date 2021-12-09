using Planetario.Models;
using System.Collections.Generic;

namespace Planetario.Handlers
{
    public interface IReportesService
    {
        List<string> ObtenerTodasLasCategorias();

        List<ProductoModel> ObtenerTodosLosProductosFiltradosPorRanking(int cantidadMostrar, string fechaInicio, string fechaFinal, string orden);

        List<ProductoModel> ObtenerTodosLosProductosFiltradosPorCategoria(string categoria, string fechaInicio, string fechaFinal);

        string ConsultaPorCategoriasPersonaExtranjeras(string categoria);

        string ConsultaPorCategoriaProductoGeneroEdad(string categoria, string genero, string publico);

        string ConsultaProductosCompradosJuntos();
    }
}