using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Planetario.Controllers
{
    public class RSSFeedController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string RSSURL)
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
            ViewBag.URL = RSSURL;
            return View();
        }
    }
}