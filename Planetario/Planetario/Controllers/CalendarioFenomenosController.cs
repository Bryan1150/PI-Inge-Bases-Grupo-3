using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planetario.Controllers
{
    public class CalendarioController : Controller
    {
        // GET: Calendario
        public ActionResult CalendarioFenomenos()
        {
            //CalendarioFenomenosHandler accesoDatos = new PreguntasFrecuentesHandler();
            //ViewBag.preguntasFrecuentes = accesoDatos.ObtenerPreguntasFrecuentes();
            return View();
        }
    }
}