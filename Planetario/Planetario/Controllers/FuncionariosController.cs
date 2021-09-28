using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Planetario.Models;
using Planetario.Handlers;

namespace Planetario.Controllers
{
    public class FuncionariosController : Controller
    {
        public ActionResult ListaFuncionarios()
        {
            FuncionariosHandler AccesoDatos = new FuncionariosHandler();
            ViewBag.listaFuncionarios = AccesoDatos.ObtenerTodosLosFuncionarios();
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerImagen(int cedula)
        {
            FuncionariosHandler AccesoDatos = new FuncionariosHandler();
            var tuple = AccesoDatos.ObtenerFoto(cedula);
            return File(tuple.Item1, tuple.Item2);
        }

    }
}