﻿using Planetario.Handlers;
using Planetario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planetario.Controllers
{
    public class JuegosController : Controller
    {
        readonly CookiesInterfaz cookiesInterfaz;

        public JuegosController()
        {
            cookiesInterfaz = new CookiesHandler();
        }

        public ActionResult principalJuegos()
        {
            return View();
        }

        public ActionResult Lista()
        {
            PersonaHandler personasHandler = new PersonaHandler();
            string correoUsuario = cookiesInterfaz.CorreoUsuario();
            string membresia = personasHandler.ObtenerMembresia(correoUsuario);
            ViewBag.Membresia = membresia;
            return View();
        }

        public ActionResult SpaceConnect()
        {
            return View();
        }

        public ActionResult memoria()
        {
            return View();
        }

        public ActionResult ahorcado()
        {
            return View();
        }

        public ActionResult rompecabezas()
        {
            return View();
        }

        public ActionResult UFO2048()
        {
            return View();
        }

        public ActionResult OuterSpaceArkanoid()
        {
            return View();
        }
    }
}