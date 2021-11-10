using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planetario.Models
{
    public class ClasesYTopicosModel
    {
        private readonly Dictionary<string, IList<SelectListItem>> DiccionarioCategorias;

        public ClasesYTopicosModel()
        {
            DiccionarioCategorias = new Dictionary<string, IList<SelectListItem>>
            {
                { "Cuerpos del sistema solar", new List<SelectListItem>(){
                    new SelectListItem { Text = "Planetas", Value = "Planetas" },
                    new SelectListItem { Text = "Satelites", Value = "Satelites"  },
                    new SelectListItem { Text = "Cometas", Value = "Cometas"  },
                    new SelectListItem { Text = "Asteroides", Value = "Asteroides"  }
                } },
                { "Objetos de Cielo Profundo", new List<SelectListItem>(){
                    new SelectListItem { Text = "Galaxias", Value = "Galaxias" },
                    new SelectListItem { Text = "Estrellas", Value = "Estrellas"  },
                    new SelectListItem { Text = "Nebulosas", Value = "Nebulosas"  },
                    new SelectListItem { Text = "Planetarias", Value = "Planetarias"  }
                } },
                { "Astronomia", new List<SelectListItem>() {
                    new SelectListItem { Text = "Astronomia Observacional", Value = "Astronomia Observacional" },
                    new SelectListItem { Text = "Astronomia Teorica", Value = "Astronomia Teorica"  },
                    new SelectListItem { Text = "Mecanica Celeste", Value = "Mecanica Celeste"  },
                    new SelectListItem { Text = "Astrofisica", Value = "Astrofisica"  },
                    new SelectListItem { Text = "Astroquimica", Value = "Astroquimica"  },
                    new SelectListItem { Text = "Astrobiologia", Value = "Astrobiologia"  }
                } },
                { "General", new List<SelectListItem>() {
                    new SelectListItem { Text = "Astrofotografia", Value = "Astrofotografia" },
                    new SelectListItem { Text = "Instrumentos", Value = "Instrumentos"  },
                    new SelectListItem { Text = "Pregunta Sencilla", Value = "Pregunta Sencilla"  }
                } }
            };
        }

        public IList<SelectListItem> ConseguirTopicosPorCategoria(string categoria)
        {
            return DiccionarioCategorias[categoria];
        }

        public Dictionary<string,

    }
}