using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class VentasController : Controller
    {

        readonly IVentasService AccesoDatos;

        public VentasController()
        {
            AccesoDatos = new VentasHandler();
        }

        public VentasController(IVentasService _servicio)
        {
            AccesoDatos = _servicio;
        }

        public ActionResult ListaProductos()
        {
            ViewBag.ListaProductos = AccesoDatos.ObtenerTodosLosProductos();
            return View();
        }

        [HttpGet]
        public ActionResult VerCarritoDelUsuario(string correoUsuario)
        {
            ViewBag.ListaProductos = AccesoDatos.ObtenerTodosLosProductosDelCarrito(correoUsuario);
            ViewBag.ListaEntradas = AccesoDatos.ObtenerTodasLasEntradasDelCarrito(correoUsuario);

            double total = 0;
            total += AccesoDatos.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario);
            total += AccesoDatos.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario);

            ViewBag.PrecioTotal = total;

            return View();
        }

        [HttpGet]
        public JsonResult EliminarElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            var exito = AccesoDatos.EliminarDelCarrito(correoUsuario, idComprable);
            return Json(new { Exito = exito }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DisminiuirLaCantidadDelElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            var exito = AccesoDatos.DisminiuirLaCantidadDelElementoDelCarrito(correoUsuario, idComprable);
            return Json(new { Exito = exito }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AumentarLaCantidadDelElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            var exito = AccesoDatos.AumentarLaCantidadDelElementoDelCarrito(correoUsuario, idComprable);
            return Json(new { Exito = exito }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AgregarProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(ProductoModel producto)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ExitoAlCrear = AccesoDatos.InsertarProducto(producto);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Mensaje = "El producto" + " " + producto.Nombre + " fue agregado con éxito";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Mensaje = "Hubo un error en el servidor";
                    }
                } 
                else
                {
                    ViewBag.Mensaje = "Hubo un error en los datos ingresados";
                }
                return View();
            }
            catch
            {
                ViewBag.Mensaje = "Hubo un error en el servidor";
                return View();
            }
        }
        
        [HttpGet]
        public JsonResult AgregarAlCarrito(int idComprable, int cantidad)
        {
            var exito = AccesoDatos.AgregarAlCarrito(idComprable, cantidad);
            return Json(new { Exito = exito }, JsonRequestBehavior.AllowGet);
        }

    }
}