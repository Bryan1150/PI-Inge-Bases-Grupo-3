using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Planetario.Handlers;
using Planetario.Models;

namespace Planetario.Controllers
{
    public class EstadisticasController : Controller
    {

        [HttpGet]
        public ActionResult verInvolucramiento()
        {
            EstadisticasHandler accesoDatos = new EstadisticasHandler();

            List<SelectListItem> listaDeDias = new List<SelectListItem>();
            List<SelectListItem> listaDePublico = new List<SelectListItem>();
            List<SelectListItem> listaDeComplejidad = new List<SelectListItem>();

            listaDeDias = listaDias();
            listaDePublico = listaPublico();
            listaDeComplejidad = listaComplejidad();

            ViewBag.listaPublico = listaDePublico;
            ViewBag.listaDias = listaDeDias;
            ViewBag.listaComplejidad = listaDeComplejidad;

            
            return View();
        }

        [HttpPost]
        public ActionResult verInvolucramiento(string opcionDia, string opcionPublico, string opcionComplejidad)
        {
            int cantidadTotalParticipantes;
            EstadisticasHandler accesoDatos = new EstadisticasHandler();


            List<SelectListItem> listaDeDias = new List<SelectListItem>();
            List<SelectListItem> listaDePublico = new List<SelectListItem>();
            List<SelectListItem> listaDeComplejidad = new List<SelectListItem>();

            cantidadTotalParticipantes = accesoDatos.obtenerCantidadDeParticipantes(opcionDia, opcionPublico, opcionComplejidad);
            listaDeDias = listaDias();
            listaDePublico = listaPublico();
            listaDeComplejidad = listaComplejidad();

            ViewBag.cantidadTotal = cantidadTotalParticipantes;
            ViewBag.listaDias = listaDeDias;
            ViewBag.listaPublico = listaDePublico;
            ViewBag.listaComplejidad = listaDeComplejidad;


            return View();
        }

        public List<SelectListItem> listaDias()
        {
            var dias = new List<String>()
            {
                "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado", "Domingo"
            };

            List<SelectListItem> listaDias = new List<SelectListItem>();
            string todos = "Todos";

            for(int i = 0; i < dias.Count(); i++)
            {
                listaDias.Add(new SelectListItem()
                {
                    Text = dias[i],
                    Selected = (dias[i] == todos ? true : false)
                }) ; 
            }

            return listaDias;
        }

        public List<SelectListItem> listaPublico()
        {
            var publico = new List<String>()
            {
                "Niños", "Jóvenes", "Adultos", "Adultos Mayores"
            };

            List<SelectListItem> listaPublico = new List<SelectListItem>();
            string todos = "Todos";

            for (int i = 0; i < publico.Count(); i++)
            {
                listaPublico.Add(new SelectListItem()
                {
                    Text = publico[i],
                    Selected = (publico[i] == todos ? true : false)
                });
            }

            return listaPublico;
        }

        public List<SelectListItem> listaComplejidad()
        {
            var complejidad = new List<String>()
            {
                "Simple", "Intermedio", "Avanzado"
            };

            List<SelectListItem> listaComplejidad = new List<SelectListItem>();
            string todos = "Todos";

            for (int i = 0; i < complejidad.Count(); i++)
            {
                listaComplejidad.Add(new SelectListItem()
                {
                    Text = complejidad[i],
                    Selected = (complejidad[i] == todos ? true : false)
                });
            }

            return listaComplejidad;
        }
    }
}