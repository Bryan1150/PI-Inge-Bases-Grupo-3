﻿using Planetario.Models;
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
        //Sat, 06 Nov 2021 17:22:13 GMT
        //2021-11-61T17:22:13
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