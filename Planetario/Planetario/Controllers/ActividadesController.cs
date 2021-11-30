using System;
using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class ActividadesController : Controller
    {
        public ActionResult CrearActividad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearActividad(ActividadModel actividad, string topicos)
        {
            ViewBag.ExitoAlCrear = false;
            string[] topicosSeleccionados = topicos.Split(';');
            try
            {
                if (ModelState.IsValid)
                {
                    ActividadHandler accesoDatos = new ActividadHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.InsertarActividad(actividad);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La actividad " + actividad.NombreActividad + " fue creada con éxito.";
                        foreach(string topico in topicosSeleccionados)
                        {
                            accesoDatos.InsertarTopico(actividad.NombreActividad, topico);
                        }
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
                ViewBag.Message = "Hubo un error al crear el cuestionario " + actividad.NombreActividad;
                return View();
            }
        }

        public ActionResult ListadoDeActividades()
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividades = accesoDatos.ObtenerActividadesAprobadas();
            return View();
        }

        public ActionResult VerActividad(string nombreDeLaActividad)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividad = accesoDatos.ObtenerActividad(nombreDeLaActividad);
            ViewBag.topicos = accesoDatos.ObtenerTopicosActividad(nombreDeLaActividad);
            ViewBag.actividades = accesoDatos.ObtenerActividadesRecomendadas(ViewBag.actividad.PublicoDirigido, ViewBag.actividad.Complejidad);
            ViewBag.entradasDisponibles = accesoDatos.ObtenerEntradasDisponiblesPorActividad(nombreDeLaActividad);
            ViewBag.asientosDisponibles = accesoDatos.ObtenerAsientos(nombreDeLaActividad);
            return View();
        }

        [HttpGet]
        public ActionResult BuscarActividad()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarActividad(string palabra)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            ViewBag.actividadesUnicas = accesoDatos.ObtenerActividadesPorBusqueda(palabra);
            return View();
        }

        [HttpGet]
        public ActionResult Inscribirme(string titulo)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            DatosHandler datosHandler = new DatosHandler();
            ViewBag.paises = datosHandler.SelectListPaises();
            ViewBag.nivelesEducativos = datosHandler.SelectListNivelesEducativos();
            ViewBag.generos = datosHandler.SelectListGeneros();
            ViewBag.precio = accesoDatos.ObtenerActividad(titulo).PrecioAproximado;
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
                    bool estaAlmacenado = accesoDatos.ParticipanteEstaAlmacenado(info.infoParticipante.correo);
                    if (!estaAlmacenado)
                        estaAlmacenado = accesoDatos.AlmacenarParticipante(info.infoParticipante);
                    if (estaAlmacenado)
                        ViewBag.exitoAlInscribir = accesoDatos.AlmacenarParticipacion(info.infoParticipante.correo, Request.Form["TituloActividad"], Double.Parse(Request.Form["PrecioActividad"]));

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
        public ActionResult VerFacturasDeActividad(string actividadNombre)
        {
            FacturasHandler accesoDatos = new FacturasHandler();
            ViewBag.facturas = accesoDatos.ObtenerFacturasDeActividad(actividadNombre);
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
                var participante = accesoDatos.ObtenerParticipante(correo);
                return Json(new{estaAlmacenado = true, infoPersonal = participante}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {estaAlmacenado = false, infoPersonal = ""}, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public JsonResult ActualizarReservacionAsiento(int fila, int columna, string correo, bool reservado)
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            bool exito = accesoDatos.ActualizarReservarAsiento(fila, columna, correo, reservado);
            return Json(new {Exito = exito }, JsonRequestBehavior.AllowGet);
        }
    }
}