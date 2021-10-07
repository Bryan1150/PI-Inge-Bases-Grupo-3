using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class PreguntasFrecuentesController : Controller {
        public ActionResult PreguntasFrecuentes() {
            PreguntasFrecuentesHandler accesoDatos = new PreguntasFrecuentesHandler();
            ViewBag.preguntasFrecuentes = accesoDatos.ObtenerPreguntasFrecuentes();
            ViewBag.categorias = accesoDatos.ObtenerCategorias();
            return View();
        }

        [HttpGet]
        public ActionResult AgregarNuevaPregunta() {                   
            return View();
        }

        [HttpPost]
        public ActionResult AgregarNuevaPregunta(PreguntasFrecuentesModel nuevaPregunta) {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid) {
                    PreguntasFrecuentesHandler accesoDatos = new PreguntasFrecuentesHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.agregarNuevaPregunta(nuevaPregunta);

                    if(ViewBag.ExitoAlCrear) {
                        ViewBag.Message = "La pregunta fue añadida satisfactoriamente!";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Sucedio algo inesperado y no fue posible añadir la pregunta.";
                return View();
            }
        }

        public ActionResult Topicos(string categoria)
        {
            switch (categoria)
            {
                case "Cuerpos del Sistema Solar":
                    return Json(
                    new List<SelectListItem>() {
                    new SelectListItem { Text = "Planetas", Value = "Planetas" },
                    new SelectListItem { Text = "Satelites", Value = "Satelites"  },
                    new SelectListItem { Text = "Cometas", Value = "Cometas"  },
                    new SelectListItem { Text = "Asteroides", Value = "Asteroides"  }              
                    },
                    JsonRequestBehavior.AllowGet
                    );
                case "Objetos de Cielo Profundo":
                    return Json(
                    new List<SelectListItem>() {
                    new SelectListItem { Text = "Galaxias", Value = "Galaxias" },
                    new SelectListItem { Text = "Estrellas", Value = "Estrellas"  },
                    new SelectListItem { Text = "Nebulosas", Value = "Nebulosas"  },
                    new SelectListItem { Text = "Planetarias", Value = "Planetarias"  }
                    },
                    JsonRequestBehavior.AllowGet
                    );
                case "Astronomia":
                    return Json(
                    new List<SelectListItem>() {
                    new SelectListItem { Text = "Astronomia Observacional", Value = "Astronomia Observacional" },
                    new SelectListItem { Text = "Astronomia Teorica", Value = "Astronomia Teorica"  },
                    new SelectListItem { Text = "Mecanica Celeste", Value = "Mecanica Celeste"  },
                    new SelectListItem { Text = "Astrofisica", Value = "Astrofisica"  },
                    new SelectListItem { Text = "Astroquimica", Value = "Astroquimica"  },
                    new SelectListItem { Text = "Astrobiologia", Value = "Astrobiologia"  }
                    },
                    JsonRequestBehavior.AllowGet
                    );
                case "General":
                    return Json(
                    new List<SelectListItem>() {
                    new SelectListItem { Text = "Astrofotografia", Value = "Astrofotografia" },
                    new SelectListItem { Text = "Instrumentos", Value = "Instrumentos"  },
                    new SelectListItem { Text = "Pregunta Sencilla", Value = "Pregunta Sencilla"  }
                    },
                    JsonRequestBehavior.AllowGet
                    );

            }
            return null;
        }
    } 
}