using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class TopicosController : Controller
    {
        private readonly ClasesYTopicosModel clasesYTopicos;

        public TopicosController ()
        {
            clasesYTopicos = new ClasesYTopicosModel();
        }

        [HttpGet]
        public ActionResult ConseguirTopicos(string categoria)
        {
            return Json( 
                clasesYTopicos.ConseguirTopicosPorCategoria(categoria) ,
                JsonRequestBehavior.AllowGet
            );
            
        }

    }
}