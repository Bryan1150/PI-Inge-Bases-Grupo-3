using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class ActividadesController : Controller
    {
        public ActionResult crearActividad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult crearActividad(ActividadModel actividad)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ActividadHandler accesoDatos = new ActividadHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearActividad(actividad);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La actividad " + actividad.NombreActividad + " fue creada con éxito.";
                        ModelState.Clear();
                    } else
                    {
                        ViewBag.Message = "Hubo un error al guardar los datos ingresados.";
                    }
                }
                else
                {
                    ViewBag.Message = "Hay un error en los datos ingresados";
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal.";
                return View();
            }
        }

        public ActionResult listadoDeActividades()
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividades = accesoDatos.obtenerTodasLasActividadesAprobadas();
            return View();
        }

        public ActionResult verActividad(string nombre)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividad = accesoDatos.buscarActividad(nombre);
            ViewBag.topicos = accesoDatos.obtenerTopicosActividades(nombre);
            ViewBag.actividades = accesoDatos.obtenerTodasLasActividadesAprobadas();
            return View();
        }

        [HttpGet]
        public ActionResult buscarActividad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult buscarActividad(string palabra)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividadesUnicas = accesoDatos.obtenerActividadBuscada(palabra);
            return View();
        }

        [HttpGet]
        public ActionResult Inscribirme(string titulo)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.precio = accesoDatos.getPrecio(titulo);
            ViewBag.titulo = titulo;
            return View();
        }

        [HttpPost]
        public ActionResult Inscribirme(InscripcionModel info)
        {
            ViewBag.exitoAlInscribir = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ParticipanteHandler accesoDatos = new ParticipanteHandler();
                    ActividadHandler actividad = new ActividadHandler();
                    bool estaAlmacenado = accesoDatos.ParticipanteEstaAlmacenado(info.infoParticipante.Correo);
                    if (!estaAlmacenado)
                        estaAlmacenado = accesoDatos.AlmacenarParticipante(info.infoParticipante);
                    if (estaAlmacenado)
                        ViewBag.exitoAlInscribir = accesoDatos.AlmacenarParticipacion(info.infoParticipante.Correo, Request.Form["TituloActividad"], Double.Parse(Request.Form["PrecioActividad"]));

                    if (ViewBag.exitoAlInscribir)
                    {
                        ViewBag.Message = "Usted ha está inscrito en la actividad " + Request.Form["TituloActividad"];
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

        [HttpGet]
        public ActionResult verFacturasDeActividad(string actividadNombre)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.facturas = accesoDatos.obtenerTodasLasFacturas(actividadNombre);
            ViewBag.nombreActividad = actividadNombre;
            return View();
        }

        [HttpGet]
        public JsonResult ValidarCorreo(string correo)
        {
            ParticipanteHandler accesoDatos = new ParticipanteHandler();
            bool existe = accesoDatos.ParticipanteEstaAlmacenado(correo);
            if (existe)
            {
                var participante = accesoDatos.GetParticipante(correo);
                return Json(new{estaAlmacenado = true, infoPersonal = participante}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {estaAlmacenado = false, infoPersonal = ""}, JsonRequestBehavior.AllowGet);
            }
            
        }

    }
}