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
            ViewBag.titulo = titulo;
            return View();
        }

        [HttpPost]
        public ActionResult Inscribirme(ParticipanteModel participante)
        {
            ViewBag.exitoAlInscribir = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ParticipanteHandler accesoDatos = new ParticipanteHandler();
                    bool estaAlmacenado = accesoDatos.ParticipanteEstaAlmacenado(participante.Correo);
                    if (!estaAlmacenado)
                        estaAlmacenado = accesoDatos.AlmacenarParticipante(participante);
                    if (estaAlmacenado)
                        ViewBag.exitoAlInscribir = accesoDatos.AlmacenarParticipacion(participante.Correo, participante.NombreActividad);

                    if (ViewBag.exitoAlInscribir)
                    {
                        ViewBag.Message = "Usted ha está inscrito en la actividad " + participante.NombreActividad;
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

        public bool PagarRecibo()
        {
            FacturaModel factura = new FacturaModel();
            factura.NombreActividad = participante.NombreActividad;
            ViewBag.exitoAlInscribir = accesoDatos.AlmacenarParticipacion(participante.Correo, participante.NombreActividad);
            if (ViewBag.exitoAlInscribir)
                {
                    ViewBag.Message = "Usted ha está inscrito en la actividad " + participante.NombreActividad;
                    ModelState.Clear();
                }
            }
        }
    }
}