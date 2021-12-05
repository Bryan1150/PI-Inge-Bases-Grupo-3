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

        public ReportesController()
        {
            AccesoDatos = new ReportesHandler();
        }

        public ReportesController(IReportesService servicio)
        {
            AccesoDatos = servicio;
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

        [HttpPost]
        public ActionResult Reporte(string categoria, string fechaInicio, string fechaFinal)
        {
            ViewBag.listadoFiltroPorCategoria = AccesoDatos.ObtenerTodosLosProductosFiltradosPorCategoria(categoria, fechaInicio, fechaFinal);
            ViewBag.listaDeCategorias = AccesoDatos.ObtenerTodasLasCategorias();
            return View("Reporte");
        }
    }
}