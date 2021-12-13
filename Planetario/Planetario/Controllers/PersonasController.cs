using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class PersonasController : Controller
    {
        [HttpGet]
        public ActionResult IniciarSesion()
        {
            String contrasena = " ";
            ViewBag.contrasena = contrasena;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(PersonaModel persona)
        {
            PersonaHandler personasHandler = new PersonaHandler();          

            if (personasHandler.EsUsuarioValido(persona.correo, persona.contrasena))
            {
                HttpCookie cookie = FormsAuthentication.GetAuthCookie(persona.correo, true);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                FormsAuthenticationTicket ticketNuevo = new FormsAuthenticationTicket(
                    ticket.Version,
                    ticket.Name,
                    ticket.IssueDate,
                    ticket.Expiration,
                    ticket.IsPersistent,
                    personasHandler.ObtenerTipoUsuario(persona.correo));

                cookie.Value = FormsAuthentication.Encrypt(ticketNuevo);
                HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("InformacionBasica", "Home");
            }
            else
            {
                ModelState.AddModelError("", "El correo o la contraseña es incorrecta");
                ViewBag.Message = "El correo o la contraseña es incorrecta.";
            }
            return View();
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("InformacionBasica", "Home");
        }
    }
}