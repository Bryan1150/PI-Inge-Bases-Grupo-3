using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class VentasController : Controller
    {

        VentasHandler AccesoDatos;

        public VentasController()
        {
            AccesoDatos = new VentasHandler();
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
        public ActionResult EliminarElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            ViewBag.ListaProductos = AccesoDatos.EliminarDelCarrito(correoUsuario, idComprable);
            return View();
        }

        [HttpGet]
        public ActionResult DisminiuirLaCantidadDelElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            ViewBag.ListaProductos = AccesoDatos.DisminiuirLaCantidadDelElementoDelCarrito(correoUsuario, idComprable);
            return View();
        }

        [HttpGet]
        public ActionResult AumentarLaCantidadDelElementoDelCarritoDelUsuario(string correoUsuario, int idComprable)
        {
            ViewBag.ListaProductos = AccesoDatos.AumentarLaCantidadDelElementoDelCarrito(correoUsuario, idComprable);
            return View();
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
                        ViewBag.Message = "El producto" + " " + producto.Nombre + " fue agregado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "El producto" + " " + producto.Nombre + " no pudo ser agregado";
                return View();
            }
        }
        
        [HttpGet]
        public ActionResult AgregarAlCarrito(int idComprable, int cantidad)
        {
            AccesoDatos.AgregarAlCarrito(idComprable, cantidad);
            return View();
        }

    }
}