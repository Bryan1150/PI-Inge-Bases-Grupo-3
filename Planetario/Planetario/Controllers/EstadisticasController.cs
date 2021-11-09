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
            List<SelectListItem> listaDeCategorias = new List<SelectListItem>();
            List<SelectListItem> listaDeTopicos = new List<SelectListItem>();

            listaDeDias = listaDias();
            listaDePublico = listaPublico();
            listaDeComplejidad = listaComplejidad();
            listaDeCategorias = listaCategorias();
            listaDeTopicos = listaTopicos();

            ViewBag.listaPublico = listaDePublico;
            ViewBag.listaDias = listaDeDias;
            ViewBag.listaComplejidad = listaDeComplejidad;
            ViewBag.listaCategorias = listaDeCategorias;
            ViewBag.listaTopicos = listaDeTopicos;

            // Gráficos

            List<int> listaParticipacionesPorDia = new List<int>();
            List<int> listaParticipacionesPorComplejidad = new List<int>();
            List<int> listaParticipacionesPorPublico = new List<int>();
            List<int> listaParticipacionesPorCategoria = new List<int>();

            foreach (var dia in listaDeDias)
            {
                listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes(dia.Text, "", "", "", ""));
            }

            foreach (var publico in listaDePublico)
            {
                listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", publico.Text, "", "", ""));
            }

            foreach (var complejidad in listaDeComplejidad)
            {
                listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", complejidad.Text, "", ""));
            }

            ViewBag.participacionesPorDia = listaParticipacionesPorDia;
            ViewBag.participacionesPorPublico = listaParticipacionesPorPublico;
            ViewBag.participacionesPorComplejidad = listaParticipacionesPorComplejidad;


            foreach(var categoria in listaDeCategorias)
            {
                listaParticipacionesPorCategoria.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", categoria.Text, ""));
            }

            ViewBag.participacionesPorCategoria = listaParticipacionesPorCategoria;

            List<int> listaParticipacionesPorTopicoTodos = new List<int>();
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Planetas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Satelites"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Cometas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Asteroides"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Galaxias"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Estrellas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Nebulosas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Planetarias"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astronomia Observacional"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astronomia Teorica"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Mecanica Celeste"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astrofisica"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astroquimica"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astrobiologia"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "General", "Astrofotografia"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "General", "Instrumentos"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "General", "Pregunta Sencilla"));
            ViewBag.participacionesPorTopicoTodos = listaParticipacionesPorTopicoTodos;

            return View();
        }

        [HttpPost]
        public ActionResult verInvolucramiento(string opcionDia, string opcionPublico, string opcionComplejidad, string opcionCategoria, string opcionTopico)
        {
            int cantidadTotalParticipantes;
            EstadisticasHandler accesoDatos = new EstadisticasHandler();

            List<SelectListItem> listaDeDias = new List<SelectListItem>();
            List<SelectListItem> listaDePublico = new List<SelectListItem>();
            List<SelectListItem> listaDeComplejidad = new List<SelectListItem>();
            List<SelectListItem> listaDeCategorias = new List<SelectListItem>();
            List<SelectListItem> listaDeTopicos = new List<SelectListItem>();
           // Dictionary<string, IList<SelectListItem>> diccionarioDeTopicos = new Dictionary<string, IList<SelectListItem>>();

            cantidadTotalParticipantes = accesoDatos.obtenerCantidadDeParticipantes(opcionDia, opcionPublico, opcionComplejidad, opcionCategoria, opcionTopico);
            listaDeDias = listaDias();
            listaDePublico = listaPublico();
            listaDeComplejidad = listaComplejidad();
            listaDeCategorias = listaCategorias();
            listaDeTopicos = listaTopicos();
            //diccionarioDeTopicos = diccionarioTopicos();

            ViewBag.listaDias = listaDeDias;
            ViewBag.listaPublico = listaDePublico;
            ViewBag.listaComplejidad = listaDeComplejidad;
            ViewBag.listaCategorias = listaDeCategorias;
            ViewBag.listaTopicos = listaDeTopicos;
            //ViewBag.diccionarioTopicos = diccionarioDeTopicos;

            ViewBag.Mensaje = stringResultado(opcionDia, opcionPublico, opcionComplejidad, opcionCategoria, opcionTopico, cantidadTotalParticipantes);

            // Gráficos

            List<int> listaParticipacionesPorDia = new List<int>();
            List<int> listaParticipacionesPorComplejidad = new List<int>();
            List<int> listaParticipacionesPorPublico = new List<int>();

            foreach (var dia in listaDeDias)
            {
                listaParticipacionesPorDia.Add(accesoDatos.obtenerCantidadDeParticipantes(dia.Text, "", "", "", ""));
            }

            foreach (var publico in listaDePublico)
            {
                listaParticipacionesPorPublico.Add(accesoDatos.obtenerCantidadDeParticipantes("", publico.Text, "", "", ""));
            }

            foreach (var complejidad in listaDeComplejidad)
            {
                listaParticipacionesPorComplejidad.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", complejidad.Text, "", ""));
            }

            ViewBag.participacionesPorDia = listaParticipacionesPorDia;
            ViewBag.participacionesPorPublico = listaParticipacionesPorPublico;
            ViewBag.participacionesPorComplejidad = listaParticipacionesPorComplejidad;

            List<int> listaParticipacionesPorCategoria = new List<int>();

            foreach (var categoria in listaDeCategorias)
            {
                listaParticipacionesPorCategoria.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", categoria.Text, ""));
            }

            ViewBag.participacionesPorCategoria = listaParticipacionesPorCategoria;

            List<int> listaParticipacionesPorTopicoTodos = new List<int>();
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Planetas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Satelites"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Cometas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Cuerpos del sistema solar", "Asteroides"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Galaxias"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Estrellas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Nebulosas"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Objetos de Cielo Profundo", "Planetarias"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astronomia Observacional"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astronomia Teorica"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Mecanica Celeste"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astrofisica"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astroquimica"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "Astronomia", "Astrobiologia"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "General", "Astrofotografia"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "General", "Instrumentos"));
            listaParticipacionesPorTopicoTodos.Add(accesoDatos.obtenerCantidadDeParticipantes("", "", "", "General", "Pregunta Sencilla"));
            ViewBag.participacionesPorTopicoTodos = listaParticipacionesPorTopicoTodos;

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

        public List<SelectListItem> listaCategorias() 
        {
            var categorias = new List<String>()
            {
                "Cuerpos del sistema solar", "Objetos de Cielo Profundo", "Astronomia", "General"
            };

            List<SelectListItem> listaCategorias = new List<SelectListItem>();
            string todos = "Todos";

            for (int i = 0; i < categorias.Count(); i++)
            {
                listaCategorias.Add(new SelectListItem()
                {
                    Text = categorias[i],
                    Selected = (categorias[i] == todos ? true : false)
                });
            }

            return listaCategorias;
        }

        
        public List<SelectListItem> listaTopicos() 
        {
            List<SelectListItem> listaTopicos = new List<SelectListItem>();
            ClasesYTopicosModel clasesYTopicos = new ClasesYTopicosModel();

            var categorias = new List<String>()
            {
                "Cuerpos del sistema solar", "Objetos de Cielo Profundo", "Astronomia", "General"
            };

            for (int i = 0; i < categorias.Count(); i++)
            {
                for (int j = 0; j < clasesYTopicos.ConseguirTopicosPorCategoria(categorias[i]).Count(); j++)
                {
                    listaTopicos.Add(clasesYTopicos.ConseguirTopicosPorCategoria(categorias[i])[j]);
                }
            }
            return listaTopicos;
        }
        

        public Dictionary<string, IList<SelectListItem>> diccionarioTopicos()
        {
            ClasesYTopicosModel clasesYTopicos = new ClasesYTopicosModel();
            return(clasesYTopicos.getDiccionario());
        }

        public string stringResultado(string dia, string publico, string complejidad, string categoria, string topico, int total)
        {
            string mensaje = "";

            mensaje = concatenarDia(mensaje, dia);
            mensaje = concatenarPublico(mensaje, publico);
            mensaje = concatenarComplejidad(mensaje, complejidad);
            mensaje = concatenarCategoria(mensaje, complejidad);
            mensaje = concatenarTopico(mensaje, complejidad);
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
                resultado += " con todos los públicos, ";
            }
            else
            {
                resultado += " con el público " + publico + ", ";
            }

            return resultado;
        }

        public string concatenarComplejidad(string resultado, string complejidad)
        {

            if (complejidad == "")
            {
                resultado += "todas las complejidades, ";
            }
            else
            {
                resultado += "la complejidad " + complejidad + ", ";
            }

            return resultado;
        }

        public string concatenarCategoria(string resultado, string categoria)
        {

            if (categoria == "")
            {
                resultado += "todas las categorias";
            }
            else
            {
                resultado += "la categoria " + categoria;
            }

            return resultado;
        }

        public string concatenarTopico(string resultado, string topico)
        {

            if (topico == "")
            {
                resultado += " y todos los topicos hay: ";
            }
            else
            {
                resultado += " y el topico " + topico + " hay: ";
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