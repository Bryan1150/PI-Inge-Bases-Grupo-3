using Microsoft.VisualStudio.TestTools.UnitTesting;
using Planetario.Models;
using Planetario.Controllers;
using Planetario.Handlers;
using Planetario.Models;
using System.Web.Mvc;
using Moq;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace PruebasPlanetarios
{
    [TestClass]
    public class ReportesControllerTest
    {
        [TestMethod]
        public void ReportesSinFiltrosNoEsNulo()
        {
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte() as ViewResult;

            Assert.IsNotNull(vistaResultado);
        }

        [TestMethod]
        public void ReportesSinFiltrosListaDeCategoriasNoEsNulo()
        {
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte() as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            Assert.IsNotNull(listaDeCategorias);
        }

        [TestMethod]
        public void ReportesSinFiltrosListaDeCategoriasEsTipoLista()
        {
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte() as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            Assert.IsInstanceOfType(listaDeCategorias, typeof(List<string>));
        }

        [TestMethod]
        public void ReportesSinFiltrosListaDeCategoriasNoTieneNulos()
        {
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte() as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            CollectionAssert.AllItemsAreNotNull(listaDeCategorias);
        }

        [TestMethod]
        public void ReportesFiltradosPorRankingNoEsNulo()
        {
            int cantidad = 0;
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            string orden = "ASC";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorRanking(cantidad, fechaInicio, fechaFinal, orden)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(cantidad, fechaInicio, fechaFinal, orden) as ViewResult;

            Assert.IsNotNull(vistaResultado);
        }

        [TestMethod]
        public void ReportesFiltradosPorRankingListaDeCategoriasNoEsNulo()
        {
            int cantidad = 0;
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            string orden = "ASC";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorRanking(cantidad, fechaInicio, fechaFinal, orden)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(cantidad, fechaInicio, fechaFinal, orden) as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            Assert.IsNotNull(listaDeCategorias);
        }

        [TestMethod]
        public void ReportesFiltradosPorRankingListaDeCategoriasEsTipoLista()
        {
            int cantidad = 0;
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            string orden = "ASC";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorRanking(cantidad, fechaInicio, fechaFinal, orden)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(cantidad, fechaInicio, fechaFinal, orden) as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            Assert.IsInstanceOfType(listaDeCategorias, typeof(List<string>));
        }

        [TestMethod]
        public void ReportesFiltradosPorRankingListaDeCategoriasNoTieneNulos()
        {
            int cantidad = 0;
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            string orden = "ASC";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorRanking(cantidad, fechaInicio, fechaFinal, orden)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(cantidad, fechaInicio, fechaFinal, orden) as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            CollectionAssert.AllItemsAreNotNull(listaDeCategorias);
        }

        [TestMethod]
        public void ReportesFiltradosPorRankingListadoFiltroPorRankingNoEsNulo()
        {
            int cantidad = 0;
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            string orden = "ASC";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorRanking(cantidad, fechaInicio, fechaFinal, orden)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(cantidad, fechaInicio, fechaFinal, orden) as ViewResult;
            var listadoFiltroPorRanking = vistaResultado.ViewBag.listadoFiltroPorRanking;

            Assert.IsNotNull(listadoFiltroPorRanking);
        }

        [TestMethod]
        public void ReportesFiltradosPorRankingListadoFiltroPorRankingEsTipoLista()
        {
            int cantidad = 0;
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            string orden = "ASC";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorRanking(cantidad, fechaInicio, fechaFinal, orden)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(cantidad, fechaInicio, fechaFinal, orden) as ViewResult;
            var listadoFiltroPorRanking = vistaResultado.ViewBag.listadoFiltroPorRanking;

            Assert.IsInstanceOfType(listadoFiltroPorRanking, typeof(List<ProductoModel>));
        }

        [TestMethod]
        public void ReportesFiltradosPorRankingListadoFiltroPorRankingNoTieneNulos()
        {
            int cantidad = 0;
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            string orden = "ASC";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorRanking(cantidad, fechaInicio, fechaFinal, orden)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(cantidad, fechaInicio, fechaFinal, orden) as ViewResult;
            var listadoFiltroPorRanking = vistaResultado.ViewBag.listadoFiltroPorRanking;

            CollectionAssert.AllItemsAreNotNull(listadoFiltroPorRanking);
        }

        [TestMethod]
        public void ReportesFiltradosPorCategoriaNoEsNulo()
        {
            string categoria = "Telescopio";
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(categoria, fechaInicio, fechaFinal) as ViewResult;

            Assert.IsNotNull(vistaResultado);
        }

        [TestMethod]
        public void ReportesFiltradosPorCategoriaListaDeCategoriasNoEsNulo()
        {
            string categoria = "Telescopio";
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(categoria, fechaInicio, fechaFinal) as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            Assert.IsNotNull(listaDeCategorias);
        }

        public void ReportesFiltradosPorCategoriaListaDeCategoriasEstipoLista()
        {
            string categoria = "Telescopio";
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(categoria, fechaInicio, fechaFinal) as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            Assert.IsInstanceOfType(listaDeCategorias, typeof(List<string>));
        }

        public void ReportesFiltradosPorCategoriaListaDeCategoriasNotieneNulos()
        {
            string categoria = "Telescopio";
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(categoria, fechaInicio, fechaFinal) as ViewResult;
            var listaDeCategorias = vistaResultado.ViewBag.listaDeCategorias;

            CollectionAssert.AllItemsAreNotNull(listaDeCategorias);
        }

        public void ReportesFiltradosPorCategoriaListadoFiltroPorCategoriaNoEsNulo()
        {
            string categoria = "Telescopio";
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(categoria, fechaInicio, fechaFinal) as ViewResult;
            var listadoFiltroPorCategoria = vistaResultado.ViewBag.listadoFiltroPorCategoria;

            Assert.IsNotNull(listadoFiltroPorCategoria);
        }

        public void ReportesFiltradosPorCategoriaListadoFiltroPorCategoriaEstipoLista()
        {
            string categoria = "Telescopio";
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(categoria, fechaInicio, fechaFinal) as ViewResult;
            var listadoFiltroPorCategoria = vistaResultado.ViewBag.listadoFiltroPorCategoria;

            Assert.IsInstanceOfType(listadoFiltroPorCategoria, typeof(List<string>));
        }

        public void ReportesFiltradosPorCategoriaListadoFiltroPorCategoriaNotieneNulos()
        {
            string categoria = "Telescopio";
            string fechaInicio = "2021/01/01";
            string fechaFinal = "2021/12/31";
            var mockReportes = new Mock<IReportesService>();
            mockReportes.Setup(servicio => servicio.ObtenerTodasLasCategorias()).Returns(new List<string>());
            mockReportes.Setup(servicio => servicio.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal)).Returns(new List<ProductoModel>());
            ReportesController reportesController = new ReportesController(mockReportes.Object);

            ViewResult vistaResultado = reportesController.Reporte(categoria, fechaInicio, fechaFinal) as ViewResult;
            var listadoFiltroPorCategoria = vistaResultado.ViewBag.listadoFiltroPorCategoria;

            CollectionAssert.AllItemsAreNotNull(listadoFiltroPorCategoria);
        }
    }
}
