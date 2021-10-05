using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;
using Planetario.Models;

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

        public ActionResult crearNoticia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult crearNoticia(NoticiaModel noticia)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    NoticiasHandler accesoDatos = new NoticiasHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearNoticia(noticia); 
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La noticia" + " " + noticia.titulo + " fue creada con éxito :)";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear la noticia :(";
                return View(); 
            }
        }
    }
}