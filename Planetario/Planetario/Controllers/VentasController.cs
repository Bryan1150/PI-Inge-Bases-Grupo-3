using System.Web.Mvc;
using System.Configuration;
using Planetario.Handlers;
using Planetario.Models;
using Planetario.Interfaces;
using System.Diagnostics;
using System;

namespace Planetario.Controllers
{
    public class VentasController : Controller
    {

        readonly VentasInterfaz ventasInterfaz;
        readonly ProductosInterfaz productosInterfaz;
        readonly CookiesInterfaz cookiesInterfaz;

        public VentasController()
        {
            ventasInterfaz = new VentasHandler();
            productosInterfaz = new ProductosHandler();
            cookiesInterfaz = new CookiesHandler();
        }

        public VentasController(VentasInterfaz _servicio)
        {
            ventasInterfaz = _servicio;
        }

        public VentasController(ProductosInterfaz _servicio)
        {
            productosInterfaz = _servicio;
        }

        public VentasController(VentasInterfaz ventas, ProductosInterfaz productos, CookiesInterfaz cookies)
        {
            if(ventas != null)
            {
                ventasInterfaz = ventas;
            }
            else
            {
                ventasInterfaz = new VentasHandler();
            }
            if(productos != null)
            {
                productosInterfaz = productos;
            }
            else
            {
                productosInterfaz = new ProductosHandler();
            }
            if(cookies != null)
            {
                cookiesInterfaz = cookies;
            }
            else
            {
                cookiesInterfaz = new CookiesHandler();
            }
        }

        public ActionResult ListaProductos()
        {
            //DatosHandler datosHandler = new DatosHandler();
            //ViewBag.categorias = datosHandler.SelectListCategorias();
            return View();
        }

        public JsonResult ListaProductosFiltrados(double precioMinimo, double precioMaximo, string categoria, string palabraBusqueda, string orden)
        {
            return Json(productosInterfaz.ObtenerProductosFiltrados(precioMinimo, precioMaximo, categoria, palabraBusqueda, orden), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Carrito()
        {            
            ActionResult resultado;
            if (cookiesInterfaz.SesionIniciada()){
                string correoUsuario;
                correoUsuario = cookiesInterfaz.CorreoUsuario();
                int cantidadEntradas = ventasInterfaz.ObtenerCantidadDeEntradasDelCarrito(correoUsuario);
                int cantidadProductos = ventasInterfaz.ObtenerCantidadDeProductosDelCarrito(correoUsuario);
                int cantidadItems = cantidadEntradas + cantidadProductos;
                double total = 0;
                Console.WriteLine(cantidadEntradas);
                ViewBag.CantidadItems = cantidadItems;

                if (cantidadEntradas != 0) 
                {
                    ViewBag.ListaEntradas = ventasInterfaz.ObtenerTodasLasEntradasDelCarrito(correoUsuario);
                    total += ventasInterfaz.ObtenerPrecioTotalDeEntradasDelCarrito(correoUsuario);
                    Console.WriteLine(total);
                }

                if (cantidadProductos != 0)
                {
                    ViewBag.ListaProductos = ventasInterfaz.ObtenerTodosLosProductosDelCarrito(correoUsuario);
                    total += ventasInterfaz.ObtenerPrecioTotalDeProductosDelCarrito(correoUsuario);
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
            bool exito = false;
            if(cookiesInterfaz.SesionIniciada())
            {
                string correoUsuario = cookiesInterfaz.CorreoUsuario();
                exito = ventasInterfaz.EliminarDelCarrito(correoUsuario, idComprable);
            }            
            return Json(new { Exito = exito }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DisminiuirLaCantidadDelElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            var exito = ventasInterfaz.DisminiuirLaCantidadDelElementoDelCarrito(correoUsuario, idComprable);
            return Json(new { Exito = exito }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AumentarLaCantidadDelElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            var exito = ventasInterfaz.AumentarLaCantidadDelElementoDelCarrito(correoUsuario, idComprable);
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
                    ViewBag.ExitoAlCrear = ventasInterfaz.InsertarProducto(producto);
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
            var exito = ventasInterfaz.AgregarAlCarrito(idComprable, cantidad);
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