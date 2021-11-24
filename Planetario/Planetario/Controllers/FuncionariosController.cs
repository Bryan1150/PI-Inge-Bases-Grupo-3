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
            EstadisticasHandler accesoDatos = new EstadisticasHandler();

            List<string> listaDeIdiomas = accesoDatos.obtenerListaIdiomas();

            List<SelectListItem> opcionIdiomas = new List<SelectListItem>();
            opcionIdiomas = obtenerIdiomas();
            ViewBag.opcionIdiomas = opcionIdiomas;

            return View();
        }

        [HttpPost]
        public ActionResult AgregarFuncionario(FuncionarioModel funcionario, string idiomas)
        {
            List<SelectListItem> opcionIdiomas = new List<SelectListItem>();
            opcionIdiomas = obtenerIdiomas();
            ViewBag.opcionIdiomas = opcionIdiomas;

            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    FuncionariosHandler accesoDatos = new FuncionariosHandler();

                    string[] idioma = idiomas.Split(';');
                    List<string> idiomasSelect = new List<string>(idioma);

                    ViewBag.ExitoAlCrear = accesoDatos.insertarFuncionario(funcionario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El funcionario "  + funcionario.Nombre + " fue agregado con éxito.";
                        foreach(var variable in idiomasSelect)
                        {
                            accesoDatos.insertarIdiomas(variable, funcionario.CorreoContacto);
                        }
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

        public List<SelectListItem> obtenerIdiomas()
        {
            EstadisticasHandler accesoDatos = new EstadisticasHandler();

            var idiomas = accesoDatos.obtenerListaIdiomas();


            List<SelectListItem> listaIdiomas = new List<SelectListItem>();
            string todos = "Todos";

            for (int i = 0; i < idiomas.Count(); i++)
            {
                listaIdiomas.Add(new SelectListItem()
                {
                    Text = idiomas[i],
                    Selected = (idiomas[i] == todos ? true : false)
                });
            }

            return listaIdiomas;
        }
    }
}