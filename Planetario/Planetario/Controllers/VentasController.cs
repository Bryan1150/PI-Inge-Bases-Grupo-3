using System.Web.Mvc;
using System.Configuration;
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
            DatosHandler datosHandler = new DatosHandler();
            ViewBag.categorias = datosHandler.SelectListCategorias();
            return View();
        }

        public JsonResult ListaProductosFiltrados(double precioMinimo, double precioMaximo, string categoria, string palabraBusqueda, string orden)
        {
            ProductosHandler productosHandler = new ProductosHandler();
            return Json(productosHandler.ObtenerProductosFiltrados(precioMinimo, precioMaximo, categoria, palabraBusqueda, orden), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Carrito()
        {            
            ActionResult resultado;
            if (Request.IsAuthenticated){
                string correoUsuario;
                correoUsuario = HttpContext.User.Identity.Name;

                int cantidadEntradas = AccesoDatos.ObtenerCantidadDeEntradasDelCarrito(correoUsuario);
                int cantidadProductos = AccesoDatos.ObtenerCantidadDeProductosDelCarrito(correoUsuario);
                int cantidadItems = cantidadEntradas + cantidadProductos;
                double total = 0;

                ViewBag.CantidadItems = cantidadItems;

                if (cantidadEntradas != 0) 
                {
                    ViewBag.ListaEntradas = AccesoDatos.ObtenerTodasLasEntradasDelCarrito(correoUsuario);
                    total += AccesoDatos.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario);
                }

                if (cantidadProductos != 0)
                {
                    ViewBag.ListaProductos = AccesoDatos.ObtenerTodosLosProductosDelCarrito(correoUsuario);
                    total += AccesoDatos.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario);
                }

                ViewBag.Precio = total;
                ViewBag.IVA = total * 0.13;
                ViewBag.PrecioTotal = ViewBag.Precio + ViewBag.IVA;
                resultado = View();
            }
            else
            {
                resultado = RedirectToAction("IniciarSesion", "Personas");                
            }
            return resultado;
        }

        [HttpGet]
        public JsonResult EliminarElementoDelCarritoDelUsuario(int idComprable)
        {
            string correoUsuario = HttpContext.User.Identity.Name;
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
        public ActionResult RealizarCompra()
        {

            DatosHandler datosHandler = new DatosHandler();
            ViewBag.paises = datosHandler.SelectListPaises();
            ViewBag.nivelesEducativos = datosHandler.SelectListNivelesEducativos();
            ViewBag.generos = datosHandler.SelectListGeneros();
            return View();
        }

        [HttpGet]
        public JsonResult AgregarAlCarrito(int idComprable, int cantidad)
        {
            var exito = AccesoDatos.AgregarAlCarrito(idComprable, cantidad);
            return Json(new { Exito = exito }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ObtenerImagen(int id)
        {
            VentasHandler ventasHandler = new VentasHandler();
            var tupla = ventasHandler.ObtenerFoto(id);
            return File(tupla.Item1, tupla.Item2);
        }
    }
}