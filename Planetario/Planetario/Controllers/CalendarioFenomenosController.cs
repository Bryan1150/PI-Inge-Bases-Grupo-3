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
            return View();
        }
        
        public JsonResult GetEventosPlanetario()
        {
            List<object> resultado = new List<object>();

            //RSS Feed
            XDocument xml = XDocument.Load("https://in-the-sky.org//rss.php?feed=dfan&latitude=9.93333&longitude=-84.08333&timezone=America/Costa_Rica");
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeedModel
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = translateFecha(((string)x.Element("pubDate")))
                               });
            foreach (RSSFeedModel evento in RSSFeedData)
            {
                resultado.Add(new
                {
                    title = evento.Title,
                    start = evento.PubDate,
                    link = evento.Link,
                    allDay = true,
                });
            }
            //Actividades Planetario
            ActividadHandler accesoDatos = new ActividadHandler();
            List<ActividadModel> actividades = accesoDatos.obtenerTodasLasActividadesAprobadas();
            
            foreach (ActividadModel actividad in actividades)
            {
                resultado.Add(new
                {
                    title = actividad.NombreActividad,
                    start = "2021-11-01"
                });
            }           
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEventosRSSFeed()
        {
            List<object> resultado = new List<object>();
            //RSS Feed
            XDocument xml = XDocument.Load("https://in-the-sky.org//rss.php?feed=dfan&latitude=9.93333&longitude=-84.08333&timezone=America/Costa_Rica");
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RSSFeedModel
                               {
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description")),
                                   PubDate = translateFecha(((string)x.Element("pubDate")))
                               });
            foreach (RSSFeedModel evento in RSSFeedData)
            {
                resultado.Add(new
                {
                    title = evento.Title,
                    start = evento.PubDate,
                    link = evento.Link,
                    allDay = true,
                });
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        //Sat, 06 Nov 2021 17:22:13 GMT
        //2021-11-6T17:22:13

        public string translateFecha(string fecha)
        {
            DateTime fechaDateTime = DateTime.Parse(fecha);
            string year = fechaDateTime.Year.ToString();
            string month = fechaDateTime.Month.ToString();
            if(fechaDateTime.Month < 10)
            {
                month = "0" + month;
            }
            string day = fechaDateTime.Day.ToString();
            if (fechaDateTime.Day < 10)
            {
               day = "0" + day;
            }
            string hour = fechaDateTime.Hour.ToString();
            string minute = fechaDateTime.Minute.ToString();
            string second = fechaDateTime.Second.ToString();

            string resultado = year + '-' + month + '-' + day; //+ 'T' + hour + ':' + minute + ':' + second
            //fechaDateTime = DateTime.Now
            return resultado;
        }
    }
}