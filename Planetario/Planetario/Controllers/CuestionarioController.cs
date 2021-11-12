using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class CuestionarioController : Controller
    {
        public ActionResult ListaCuestionarios()
        {
            CuestionarioHandler AcessoDatos = new CuestionarioHandler();
            ViewBag.ListaCuestionarios = AcessoDatos.obtenerCuestinariosSimple();
            return View();
        }

        public ActionResult VerCuestionario(string nombre)
        {
            CuestionarioHandler AcessoDatos = new CuestionarioHandler();
            ViewBag.Cuestionario = AcessoDatos.buscarCuestionario(nombre);
            return View();
        }

        public ActionResult agregarCuestionario()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult agregarCuestionario(CuestionarioModel cuestionario)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    CuestionarioHandler accesoDatos = new CuestionarioHandler();
                    cuestionario.EmbedHTML = modificarEmbed(cuestionario.EmbedHTML);
                    ViewBag.ExitoAlCrear = accesoDatos.agregarCuestionario(cuestionario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El cuestionario " + cuestionario.NombreCuestionario + " fue creada con éxito.";
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

        public string modificarEmbed(string embed)
        {
            int inicio = embed.IndexOf("https");
            embed = embed.Substring(inicio, embed.IndexOf("true") - inicio + 4);
            return embed;
        }
    }
}