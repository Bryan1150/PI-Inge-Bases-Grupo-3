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

        [Required(ErrorMessage = "Es necesario que le indique el nombre que va a tener la actividad.")]
        [Display(Name = "Nombre de la actividad:")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el tema de la actividad.")]
        [Display(Name = "Tema de la actividad:")]
        public string tema { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la descripcion de la actividad.")]
        [Display(Name = "Descripcion de la actividad:")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el tipo de la actividad.")]
        [Display(Name = "Tipo de la actividad:")]
        public string tipo { get; set; }

        [Required(ErrorMessage = "Es necesario que indique a cual publico esta dirigida la actividad.")]
        [Display(Name = "Ppublico dirigido:")]
        public string publicoDirigido { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese aproximadamente cuantos minutos durara la actividad.")]
        [Display(Name = "Duración:")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar números")]
        public int duracion { get; set; }

        [Required(ErrorMessage = "Es necesario que indique su correo.")]
        [Display(Name = "Ingrese su correo:")]
        public string correoFK { get; set; }
    }
}