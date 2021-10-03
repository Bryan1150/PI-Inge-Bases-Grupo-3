﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;

namespace Planetario.Controllers
{
    public class NoticiasController : Controller
    {
        public ActionResult listadoDeNoticias()
        {
            NoticiasHandler accesoDatos = new NoticiasHandler();
            ViewBag.noticias = accesoDatos.obtenerTodasLasNoticias();
            return View();
        }

        public ActionResult verNoticia(string stringId)
        {
            NoticiasHandler accesoDatos = new NoticiasHandler();
            ViewBag.noticia = accesoDatos.buscarNoticia(stringId);
            return View();
        }
    }
}