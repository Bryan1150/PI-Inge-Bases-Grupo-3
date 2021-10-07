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
                        ViewBag.Message = "La actividad " + actividad.nombre + " fue creada con éxito.";
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
            ViewBag.actividades = accesoDatos.obtenerTodasLasActividades();
            return View();
        }
    }
}