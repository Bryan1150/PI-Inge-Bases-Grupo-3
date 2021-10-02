using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class FuncionarioModel
    {
        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "Es necesario que ingrese una cedula")]
        public int Cedula { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es necesario que ingrese una nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "Es necesario que ingrese al menos un apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "Fecha de incorporación")]
        [Required(ErrorMessage = "Es necesario que ingrese la fecha en la que empezó a trabajar el funcionario")]
        public string FechaIncorporacion { get; set; }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Es necesario que ingrese el rol del funcionario")]
        public string RolTrabajo { get; set; }

        [Display(Name = "Correo")]
        public string CorreoContacto { get; set; }

        [Display(Name = "Foto del funcionario")]
        public HttpPostedFileBase Foto { get; set; }

        public string TipoArchivoFoto { get; set; }

    }
}