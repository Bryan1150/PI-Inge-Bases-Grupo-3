using Planetario.Handlers;
using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planetario.Controllers
{
    public class EvaluacionController : Controller
    {
        public ActionResult VerCuestionarioEvaluacion()
        {
            EvaluacionHandler AcessoDatos = new EvaluacionHandler();
            ViewBag.Cuestionario = AcessoDatos.ObtenerCuestionario("Califica tu experiencia");
            return View();
        }

        [HttpPost]
        public ActionResult RespuestaCuestionario(CuestionarioEvaluacionRecibirModel cuestionario)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    EvaluacionHandler accesoDatos = new EvaluacionHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.InsertarRespuestas(cuestionario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "Se respondio el cuestionario con exito.";                   
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Hubo un error al insertar las respuestas.";
                    }
                }
                else
                {
                    ViewBag.Message = "El modelo introducido no es valido.";
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Hubo un error al responder el cuestionario.";
                return View();
            }
        }
    }
}