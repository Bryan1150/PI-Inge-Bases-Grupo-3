﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Planetario.Handlers;

namespace Planetario.Controllers
{
    public class MaterialEducativoController : Controller
    {
        public ActionResult ListaMaterialesEducativos()
        {
            MaterialesEducativosHandler AccesoDatos = new MaterialesEducativosHandler();
            ViewBag.listaMateriales = AccesoDatos.ObtenerInfoTodosLosMateriales();
            return View();
        }
    }
}