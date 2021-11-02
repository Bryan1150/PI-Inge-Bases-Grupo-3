using Planetario.Models;
using Planetario.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Planetario.Controllers
{
    public class CalendarioController : Controller
    {
        // GET: Calendario
        public ActionResult CalendarioFenomenos()
        {
            XDocument xml = XDocument.Load("https://in-the-sky.org//rss.php?feed=dfan&latitude=9.93333&longitude=-84.08333&timezone=America/Costa_Rica");
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeedModel
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = ((string)x.Element("pubDate"))
                               });
            ViewBag.RSSFeed = RSSFeedData;
            return View();
        }

        public JsonResult GetEventosPlanetario()
        {
            ActividadHandler accesoDatos = new ActividadHandler();
            List<ActividadModel> actividades = accesoDatos.obtenerTodasLasActividadesAprobadas();
            List<object> resultado = new List<object>();
            foreach (ActividadModel actividad in actividades)
            {
                resultado.Add(new
                {
                    title = actividad.NombreActividad,
                    start = "2020-09-01"
                });
            }
            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventsForCalendar()
        {
            XDocument xml = XDocument.Load("https://in-the-sky.org//rss.php?feed=dfan&latitude=9.93333&longitude=-84.08333&timezone=America/Costa_Rica");
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeedModel
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = ((string)x.Element("pubDate"))
                               });
           
            return Json(RSSFeedData, JsonRequestBehavior.AllowGet);
        }
    }
}