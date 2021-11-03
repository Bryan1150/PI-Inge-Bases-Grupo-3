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
    public class CuestionarioController : Controller
    {
        public ActionResult ListaCuestionarios()
        {
            CuestionarioHandler AcessoDatos = new CuestionarioHandler();
            ViewBag.ListaCuestionarios = AcessoDatos.obtenerCuestinariosSimple();
            return View();
        }

        public ActionResult VerCuestionario(string hmtl)
        {
            CuestionarioHandler AcessoDatos = new CuestionarioHandler();
            ViewBag.Cuestionario = AcessoDatos.buscarCuestionario(hmtl);
            return View();
        }
    }
}