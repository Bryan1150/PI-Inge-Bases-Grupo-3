using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Interfaces;

namespace Planetario.Controllers
{
    public class MembresiasController : Controller
    {
        readonly MembresiasInterfaz membresiasInterfaz;
        readonly CookiesInterfaz cookiesInterfaz;

        public MembresiasController()
        {
            membresiasInterfaz = new MembresiasHandler();
            cookiesInterfaz = new CookiesHandler();
        }

        public MembresiasController(MembresiasInterfaz ventas, CookiesInterfaz cookies)
        {
            membresiasInterfaz = ventas;
            cookiesInterfaz = cookies;
        }

        public ActionResult Comprar()
        {          
            PersonaHandler personasHandler = new PersonaHandler();
            string correoUsuario = cookiesInterfaz.CorreoUsuario();
            string membresia = personasHandler.ObtenerMembresia(correoUsuario);
            ViewBag.Membresia = membresia;
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
            string correo = cookiesInterfaz.CorreoUsuario();
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