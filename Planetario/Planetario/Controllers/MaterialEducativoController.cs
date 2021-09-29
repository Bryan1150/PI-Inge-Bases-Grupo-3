using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class MaterialEducativoController : Controller
    {
        public ActionResult AlmacenarNuevoMaterialEducativo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlmacenarNuevoMaterialEducativo(MaterialEducativoModel Material)
        {
            ViewBag.ExitoAlmacenar = false;
            try
            {
                if(ModelState.IsValid)
                {
                    MaterialesEducativosHandler AccesoADatos = new MaterialesEducativosHandler();
                    ViewBag.ExitoAlmacenar = AccesoADatos.AlmacenarMaterialEducativo(Material);
                    if(ViewBag.ExitoAlmacenar)
                    {
                        ViewBag.Mensaje = "El material " + Material.Titulo + "fue almacenado con exito 😉";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Messasge = "Algo salió mal y no fue posible crear el planeta 😐";
                return View();
            }
        }

    }
}