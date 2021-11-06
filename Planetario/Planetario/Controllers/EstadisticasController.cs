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

            // Gráficos

            List<int> listaParticipacionesPorDia = new List<int>();
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("lunes", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("martes", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("miercoles", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("jueves", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("viernes", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("sabado", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("domingo", "", ""));
            ViewBag.participacionesPorDia = listaParticipacionesPorDia;


            List<int> listaParticipacionesPorPublico = new List<int>();
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "niños", ""));
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "jóvenes", ""));
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "adultos", ""));
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "adultos mayores", ""));
            ViewBag.participacionesPorPublico = listaParticipacionesPorPublico;

            List<int> listaParticipacionesPorComplejidad = new List<int>();
            listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "simple"));
            listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "intermedio"));
            listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "avanzado"));
            ViewBag.participacionesPorComplejidad = listaParticipacionesPorComplejidad;

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

            ViewBag.listaDias = listaDeDias;
            ViewBag.listaPublico = listaDePublico;
            ViewBag.listaComplejidad = listaDeComplejidad;

            ViewBag.Mensaje = stringResultado(opcionDia, opcionPublico, opcionComplejidad, cantidadTotalParticipantes);

            // Gráficos

            List<int> listaParticipacionesPorDia = new List<int>();
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("lunes", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("martes", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("miercoles", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("jueves", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("viernes", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("sabado", "", ""));
            listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes("domingo", "", ""));
            ViewBag.participacionesPorDia = listaParticipacionesPorDia;


            List<int> listaParticipacionesPorPublico = new List<int>();
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "niños", ""));
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "jóvenes", ""));
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "adultos", ""));
            listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", "adultos mayores", ""));
            ViewBag.participacionesPorPublico = listaParticipacionesPorPublico;

            List<int> listaParticipacionesPorComplejidad = new List<int>();
            listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "simple"));
            listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "intermedio"));
            listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "avanzado"));
            ViewBag.participacionesPorComplejidad = listaParticipacionesPorComplejidad;

            return View();
        }

        [HttpGet]
        public ActionResult verIdiomas()
        {
            EstadisticasHandler accesoDatos = new EstadisticasHandler();

            List<string> listaDeIdiomas = accesoDatos.obtenerListaIdiomas();
            List<int> listaNumIdiomas = new List<int>();

            foreach (var idioma in listaDeIdiomas)
            {
                listaNumIdiomas.Add(accesoDatos.obtenerNumIdiomas(idioma));
            }

            ViewBag.listaIdiomas = listaDeIdiomas;
            ViewBag.listaNumIdiomas = listaNumIdiomas;

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

        public string stringResultado(string dia, string publico, string complejidad, int total)
        {
            string mensaje = "";

            mensaje = concatenarDia(mensaje, dia);
            mensaje = concatenarPublico(mensaje, publico);
            mensaje = concatenarComplejidad(mensaje, complejidad);
            mensaje = concatenarTotal(mensaje, total);

            return mensaje;
        }

        public string concatenarDia(string resultado, string dia) {

            if (dia == "")
            {
                resultado += "Todos los días, ";
            }
            else
            {
                resultado += "El día " + dia;
            }

            return resultado;
        }

        public string concatenarPublico(string resultado, string publico)
        {

            if (publico == "")
            {
                resultado += " con todos los públicos ";
            }
            else
            {
                resultado += " con el público " + publico;
            }

            return resultado;
        }

        public string concatenarComplejidad(string resultado, string complejidad)
        {

            if (complejidad == "")
            {
                resultado += " y todas las complejidades hay ";
            }
            else
            {
                resultado += " y la complejidad " + complejidad + " hay ";
            }

            return resultado;
        }

        public string concatenarTotal(string resultado, int total)
        {

            if (total != 1)
            {
                resultado += total + " paticipantes";
            }
            else
            {
                resultado += total + " participante";
            }

            return resultado;
        }
    }
}