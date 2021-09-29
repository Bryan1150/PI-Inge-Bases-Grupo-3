using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Planetario.Handlers;

namespace Planetario.Controllers
{
    public class FuncionariosController : Controller
    {
        public ActionResult ListaFuncionarios()
        {
            FuncionariosHandler AcessoDatos = new FuncionariosHandler();
            ViewBag.ListaFuncionarios = AcessoDatos.ObtenerTodosLosFuncionarios();
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerImagen(int cedula)
        {
            FuncionariosHandler productHandler = new FuncionariosHandler();
            var tuple = productHandler.ObtenerFoto(cedula);
            return File(tuple.Item1, tuple.Item2);
        }

        public ActionResult AgregarFuncionario()
        {
            return View("AgregarFuncionario");
        }


        [HttpPost]
        public ActionResult AgregarFuncionario(FuncionarioModel funcionario)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    FuncionariosHandler accesoDatos = new FuncionariosHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearFuncionario(funcionario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El funcionario" + " " + funcionario.Nombre + " fue agregado con éxito:)";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal ";
                return View();
            }
        }


    }
}