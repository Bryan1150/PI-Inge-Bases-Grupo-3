using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class ActividadesController : Controller
    {
        public ActionResult crearActividad()
        {
            return View();
        }
        [HttpPost]
        public ActionResult crearActividad(ActividadModel actividad)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if(ModelState.IsValid)
                {
                    ActividadHandler accesoDatos = new ActividadHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearActividad(actividad);
                    if(ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La actividad " + actividad.NombreActividad + " fue creada con éxito.";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal.";
                return View();
            }
        }

        public ActionResult listadoDeActividades()
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividades = accesoDatos.obtenerTodasLasActividadesAprobadas();
            return View();
        }

        public ActionResult verActividad(string nombre)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividad = accesoDatos.buscarActividad(nombre);
            ViewBag.topicos = accesoDatos.obtenerTopicosActividades(nombre);
            return View();
        }

        [HttpGet]
        public ActionResult buscarActividad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult buscarActividad(string palabra)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividadesUnicas = accesoDatos.obtenerActividadBuscada(palabra);
            return View();
        }
    }
}