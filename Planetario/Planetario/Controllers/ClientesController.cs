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
    public class ClientesController : Controller
    {

        [HttpGet]
        public ActionResult Registro()
        {
            DatosHandler dataHandler = new DatosHandler();
            ViewBag.paises = dataHandler.SelectListPaises();
            ViewBag.generos = dataHandler.SelectListGeneros();
            return View();
        }

        [HttpPost]
        public ActionResult Registro(PersonaModel persona)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ClientesHandler accesoDatos = new ClientesHandler();

                    ViewBag.ExitoAlCrear = accesoDatos.InsertarCliente(persona);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = persona.nombre + " tu registro fue exitoso.";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("InformacionBasica", "Home");
            }
            catch
            {
                ViewBag.Message = "No pudimos completar tu registro.";
                return View();
            }
        }
    }
}