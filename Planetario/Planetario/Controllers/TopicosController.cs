using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Handlers;

namespace Planetario.Controllers
{
    public class TopicosController : Controller
    {
        [HttpGet]
        public ActionResult ConseguirTopicos(string categoria)
        {
            DatosHandler dataHandler =  new DatosHandler();
            return Json(dataHandler.ObtenerTopicosPorCategoria(categoria) ,JsonRequestBehavior.AllowGet);           
        }
    }
}