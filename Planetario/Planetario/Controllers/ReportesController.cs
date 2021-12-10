using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class ReportesController : Controller
    {
        readonly IReportesService AccesoDatos;
        readonly DatosHandler AccesoDatosApp;

        public ReportesController()
        {
            AccesoDatos = new ReportesHandler();
            AccesoDatosApp = new DatosHandler();
        }

        public ReportesController(IReportesService servicio)
        {
            AccesoDatos = servicio;
            AccesoDatosApp = new DatosHandler();
        }

        [HttpGet]
        public ActionResult Reporte()
        {
            ViewBag.listaDeCategorias = AccesoDatos.ObtenerTodasLasCategorias();
            return View("Reporte");
        }

        
        [HttpPost]
        public ActionResult Reporte(int cantidadMostrar, string fechaInicio, string fechaFinal, string orden)
        {
            ViewBag.listadoFiltroPorRanking = AccesoDatos.ObtenerTodosLosProductosFiltradosPorRanking(cantidadMostrar, fechaInicio, fechaFinal, orden);
            ViewBag.listaDeCategorias = AccesoDatos.ObtenerTodasLasCategorias();
            return View("Reporte");
        }

        /**
        [HttpPost]
        public ActionResult Reporte(string categoria, string fechaInicio, string fechaFinal)
        {
            ViewBag.listadoFiltroPorCategoria = AccesoDatos.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal);
            ViewBag.listaDeCategorias = AccesoDatos.ObtenerTodasLasCategorias();
            return View("Reporte");
        }
        */


        public ActionResult ReporteMercadeo()
        {
            ViewBag.listaDeCategorias = AccesoDatos.ObtenerTodasLasCategorias();
            ViewBag.listaGeneros = AccesoDatosApp.SelectListGeneros();
            ViewBag.listaPublicos = AccesoDatosApp.SelectListPublicos();
            return View("ReporteMercadeo");
        }

        [HttpGet]
        public JsonResult ObtenerDatosExtranjeros(string categoria)
        {
            var resultadoJson = AccesoDatos.ConsultaPorCategoriasPersonaExtranjeras(categoria);
            return Json(resultadoJson, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerDatosPorGeneroYEdad(string categoria, string genero, string publico)
        {
            var resultadoJson = AccesoDatos.ConsultaPorCategoriaProductoGeneroEdad(categoria, genero, publico);
            return Json(resultadoJson, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerParesProductos()
        {
            var resultadoJson = AccesoDatos.ConsultaProductosCompradosJuntos();
            return Json(resultadoJson, JsonRequestBehavior.AllowGet);
        }
    }
}