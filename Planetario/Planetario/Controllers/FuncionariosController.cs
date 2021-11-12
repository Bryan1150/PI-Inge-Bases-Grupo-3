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
    public class FuncionariosController : Controller
    {
        public ActionResult ListaFuncionarios()
        {
            FuncionariosHandler AcessoDatos = new FuncionariosHandler();
            ViewBag.ListaFuncionarios = AcessoDatos.obtenerFuncionariosSimple();
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerImagen(string correo)
        {
            FuncionariosHandler productHandler = new FuncionariosHandler();
            var tupla = productHandler.ObtenerFoto(correo);
            return File(tupla.Item1, tupla.Item2);
        }


        public ActionResult VerFuncionario(string correo)
        {
            FuncionariosHandler AcessoDatos = new FuncionariosHandler();
            ViewBag.Funcionario = AcessoDatos.buscarFuncionario(correo);
            ViewBag.Idiomas = AcessoDatos.obtenerIdiomasFuncionario(correo);
            ViewBag.Titulos = AcessoDatos.obtenerTitulosFuncionario(correo);
            ViewBag.Roles = AcessoDatos.obtenerRolesFuncionario(correo);

            return View();
        }

        [HttpGet]
        public ActionResult iniciarSesion()
        {
            String contrasena = " ";
            ViewBag.contrasena = contrasena;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult iniciarSesion(FuncionarioModel funcionario)
        {
            FuncionariosHandler funcionarioHandler = new FuncionariosHandler();


            if (funcionarioHandler.esFuncionarioValido(funcionario.Contrasena, funcionario.CorreoContacto))
            {
                FormsAuthentication.SetAuthCookie(funcionario.CorreoContacto, false);
                return RedirectToAction("InformacionBasica", "Home");

            }
            else
            {
                ModelState.AddModelError("", "El correo o la contraseña es incorrecta");
                ViewBag.Message = "El correo o la contraseña es incorrecta.";
            }
            return View();
        }

        public ActionResult cerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("InformacionBasica", "Home");
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
                    //ViewBag.ExitoAlCrear = accesoDatos.crearFuncionario(funcionario);
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