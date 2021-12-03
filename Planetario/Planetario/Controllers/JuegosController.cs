using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planetario.Controllers
{
    public class JuegosController : Controller
    {
        public ActionResult principalJuegos()
        {
            return View();
        }

        // GET: Juegos
        public ActionResult memoria()
        {
            return View();
        }

        public ActionResult ahorcado()
        {
            return View();
        }

        public ActionResult rompecabezas()
        {
            return View();
        }
    }
}