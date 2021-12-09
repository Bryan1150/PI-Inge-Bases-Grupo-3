using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;

namespace Planetario.Controllers
{
    public class MembresiasController : Controller
    {
        public ActionResult Comprar()
        {
            return View();
        }

        public ActionResult Pago(string membresia)
        {
            ViewBag.Membresia = membresia;

            if(membresia == "Lunar")
            {
                ViewBag.Precio = 5000;
                ViewBag.IVA = 650;
                ViewBag.PrecioTotal = 5650;
            }
            if (membresia == "Solar")
            {
                ViewBag.Precio = 10000;
                ViewBag.IVA = 1300;
                ViewBag.PrecioTotal = 11300;
            }
            return View();
        }

        public ActionResult Satisfactorio(string membresia)
        {
            CookiesHandler correoHandler = new CookiesHandler();
            string correo = correoHandler.CorreoUsuario();
            MembresiasHandler membresiaHandler = new MembresiasHandler();

            ViewBag.ExitoAlActualizar = false;
            try
            {
                ViewBag.ExitoAlCrear = membresiaHandler.ActualizarMembresia(correo, membresia);
                if (ViewBag.ExitoAlCrear)
                {
                    ViewBag.Mensaje = "Su membresía ahora es: " + membresia + "!";
                }
                else
                {
                    ViewBag.Mensaje = "Hubo un error en el servidor";
                }

                return View();
            }
            catch
            {
                ViewBag.Mensaje = "Hubo un error en el servidor";
                return View();
            }
        }
    }
}