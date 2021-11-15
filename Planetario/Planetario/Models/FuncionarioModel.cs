using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Planetario.Models
{
    public class FuncionarioModel
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "Es necesario que ingrese su correo electrónico")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CorreoContacto { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es necesario que ingrese su nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "Es necesario que ingrese su primer apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "Ingrese su descripción")]
        [Required(ErrorMessage = "Es necesario que ingrese su descripción")]
        [MaxLength(10000, ErrorMessage = "Se tiene un máximo de 200 cáracteres")]
        public string Descripcion { get; set; }

        [Display(Name = "Ingrese su país")]
        [Required(ErrorMessage = "Es necesario que ingrese su país")]
        public string Pais { get; set; }

        [Display(Name = "Fecha de incorporación")]
        public string FechaIncorporacion { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "Es necesario que ingrese su género")]
        public string Genero { get; set; }

        [Display(Name = "Area expertis")]
        [Required(ErrorMessage = "Es necesario que ingrese el área en que es experto")]
        public string AreaExpertis { get; set; }

        [Display(Name = "Idiomas")]
        //[Required(ErrorMessage = "Es necesario que ingrese al menos un idioma que domine")]
        public IList<string> Idiomas { get; set; }

        [Display(Name = "Roles")]
        //[Required(ErrorMessage = "Es necesario que ingrese al menos un rol que va a ejercer")]
        public IList<string> Roles { get; set; }

        [Display(Name = "Títulos")]
        //[Required(ErrorMessage = "Es necesario que ingrese al menos un título")]
        public IList<string> Titulos { get; set; }

        [Display(Name = "Foto del funcionario")]
        public HttpPostedFileBase FotoArchivo { get; set; }

        public string TipoArchivoFoto { get; set; }

        [Display(Name = "Contrasena")]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        [Required(ErrorMessage = "Es necesario que ingrese su contrasena")]
        public string Contrasena { get; set; }

        public FuncionarioModel()
        {
            Idiomas = new List<string>();
            Roles = new List<string>();
            Titulos = new List<string>();
        }
    }
}