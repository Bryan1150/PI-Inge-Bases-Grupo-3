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

        public ActionResult VerCuestionario(string nombre)
        {
            CuestionarioHandler AcessoDatos = new CuestionarioHandler();
            ViewBag.Cuestionario = AcessoDatos.buscarCuestionario(nombre);
            return View();
        }

        public ActionResult agregarCuestionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult agregarCuestionario(CuestionarioModel cuestionario)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    CuestionarioHandler accesoDatos = new CuestionarioHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.agregarCuestionario(cuestionario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El cuestionario " + cuestionario.NombreCuestionario + " fue creada con éxito.";
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

        /*
        [HttpPost]
        public ActionResult crearCuestionario(CuestionarioModel cuestionario)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ActividadHandler accesoDatos = new ActividadHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearCuestionario(cuestionario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La actividad " + cuestionario.NombreCuestionario + " fue creada con éxito.";
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
        **/
    }
}