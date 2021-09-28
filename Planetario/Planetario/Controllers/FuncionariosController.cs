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
    }
}