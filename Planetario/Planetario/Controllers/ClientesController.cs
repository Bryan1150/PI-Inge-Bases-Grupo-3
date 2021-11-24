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
        public ActionResult Inscribirme()
        {
            DatosHandler datosHandler = new DatosHandler();
            ViewBag.paises = datosHandler.SelectListPaises();
            ViewBag.nivelesEducativos = datosHandler.SelectListNivelesEducativos();
            ViewBag.generos = datosHandler.SelectListGeneros();
            return View();
        }

        [HttpPost] 
        public ActionResult Inscribirme(ClienteModel cliente)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ClientesHandler accesoDatos = new ClientesHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.InsertarCliente(cliente);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "Su cuenta " + cliente.correo + " ha sido creada con éxito :)";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("InformacionBasica", "Home");
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear su cuenta :(";
                return RedirectToAction("InformacionBasica", "Home");
            }
        }
    }
}