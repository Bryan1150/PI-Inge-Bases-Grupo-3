using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Planetario.Models
{
    public class ActividadModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario que le indique el nombre que va a tener la actividad")]
        [Display(Name = "Ingrese el nombre de la actividad")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el tema de la actividad")]
        [Display(Name = "Ingrese el tema de la actividad")]
        public string tema { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la descripcion de la actividad")]
        [Display(Name = "Ingrese la descripcion de la actividad")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el contenido de la actividad")]
        [Display(Name = "Ingrese el contenido de la actividad")]
        public string contenido { get; set; }

        [Required(ErrorMessage = "Es necesario que indique a cual publico esta dirigida la actividad")]
        [Display(Name = "Ingrese a cual publico esta dirigida la actividad")]
        public string publicoDirigido { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese aproximadamente cuantos minutos durara la actividad")]
        [Display(Name = "Ingrese aproximadamente cuantos minutos durara la actividad")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar números")]
        public int duracion { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese aproximadamente cuanta gente se provee que va asistir")]
        [Display(Name = "Ingrese el número de anillos de la actividad")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar números")]
        public int cantidadAsistentes { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el tipo de la actividad")]
        [Display(Name = "Ingrese el tipo de la actividad")]
        public string correoFK { get; set; }
    }
}