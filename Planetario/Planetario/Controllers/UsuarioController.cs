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
    public class UsuarioController : Controller
    {
        public ActionResult Usuario()
        {
            UsuarioHandler accesoDatos = new UsuarioHandler();
            ViewBag.usuario = accesoDatos.obtenerUsuarios();
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
        public ActionResult iniciarSesion(UsuarioModel usuario)
        {
            UsuarioHandler usuarioHandler = new UsuarioHandler();


            if (usuarioHandler.esUsuarioValido(usuario.contrasena, usuario.correo))
            {
                FormsAuthentication.SetAuthCookie(usuario.correo, false);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "El correo o la contraseña es incorrecta");
            }
            return View(usuario);
        }

        public ActionResult cerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult agregarUsuario()
        {
            return View("agregarUsuario");
        }

        [HttpPost]
        public ActionResult agregarUsuario(UsuarioModel usuario)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioHandler accesoDatos = new UsuarioHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.insertarUsuario(usuario);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El funcionario " + usuario.nombre + " fue agregado con éxito.";
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