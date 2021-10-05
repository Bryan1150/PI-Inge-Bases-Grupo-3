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
            var tupla = productHandler.ObtenerFoto(cedula);
            return File(tupla.Item1, tupla.Item2);
        }

        [HttpGet]
        public ActionResult AgregarFuncionario()
        {
            return View();
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
                        ViewBag.Message = "El funcionario "  + funcionario.Nombre + " fue agregado con éxito.";
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