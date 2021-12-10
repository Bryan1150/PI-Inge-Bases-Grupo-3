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
            ViewBag.Cuestionario = acessoDatos.ObtenerCuestionarioRecibir("Califica tu experiencia");
            return View();
        }

        [HttpPost]
        public ActionResult CuestionarioEvaluacion(CuestionarioEvaluacionRecibirModel cuestionario)
        {
            ViewBag.ExitoAlCrear = false;
            EvaluacionHandler accesoDatos = new EvaluacionHandler();
            ViewBag.Cuestionario = accesoDatos.ObtenerCuestionarioRecibir("Califica tu experiencia");
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

        public ActionResult MostrarCuestionarioEvaluacion()
        {
            EvaluacionHandler accesoDatos = new EvaluacionHandler();
            ViewBag.Cuestionario = accesoDatos.ObtenerCuestionarioMostrar("Califica tu experiencia");
            ViewBag.Cantidad = accesoDatos.ObtenerCantidadPersonas("Califica tu experiencia");

            DatosController datos = new DatosController();
            ViewBag.Opciones = datos.RespuestasEvaluacion();

            ViewBag.RespuestasEsteticamente = accesoDatos.ObtenerCantidadRespuestasPorPregunta(1);
            ViewBag.RespuestasNavegar = accesoDatos.ObtenerCantidadRespuestasPorPregunta(2);
            ViewBag.RespuestasComprar = accesoDatos.ObtenerCantidadRespuestasPorPregunta(3);
            ViewBag.RespuestasPrecios = accesoDatos.ObtenerCantidadRespuestasPorPregunta(4);
            ViewBag.RespuestasSatisfecho = accesoDatos.ObtenerCantidadRespuestasPorPregunta(5);

            return View();
        }
    }
}