using Planetario.Handlers;
using Planetario.Models;
using System.Web.Mvc;

namespace Planetario.Controllers
{
    public class EvaluacionController : Controller
    {
        public ActionResult CuestionarioEvaluacion()
        {
            EvaluacionHandler acessoDatos = new EvaluacionHandler();
            ViewBag.Cuestionario = acessoDatos.ObtenerCuestionario("Califica tu experiencia");
            return View();
        }

        [HttpPost]
        public ActionResult CuestionarioEvaluacion(CuestionarioEvaluacionRecibirModel cuestionario)
        {
            ViewBag.ExitoAlCrear = false;
            EvaluacionHandler accesoDatos = new EvaluacionHandler();
            ViewBag.Cuestionario = accesoDatos.ObtenerCuestionario("Califica tu experiencia");
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ExitoAlCrear = accesoDatos.InsertarRespuestas(cuestionario);
                    if(cuestionario.Comentario[0] != "")
                        ViewBag.ExitoAlCrear = accesoDatos.InsertarComentario(cuestionario);
                    ViewBag.ExitoAlCrear = accesoDatos.InsertarFuncionalidadesEvaluadas(cuestionario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "Se respondió el cuestionario con exito.";                   
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Hubo un error al insertar las respuestas.";
                    }
                }
                else
                {
                    ViewBag.Message = "El cuestionario tiene errores. Por favor revise sus respuestas.";
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