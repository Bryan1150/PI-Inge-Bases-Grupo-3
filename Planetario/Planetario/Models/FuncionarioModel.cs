using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class FuncionarioModel
    {
        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "Es necesario que ingrese el número de una cédula")]
        public int Cedula { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "Fecha de incorporación")]
        [Required(ErrorMessage = "Es necesario que ingrese la fecha en la que empezó a trabajar el funcionario")]
        public string FechaIncorporacion { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Es necesario que ingrese el rol del funcionario")]
        public string RolTrabajo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoContacto { get; set; }

        [Display(Name = "Foto del funcionario")]
        [Required(ErrorMessage = "Es necesario que ingrese una foto del funcionario")]
        public HttpPostedFileBase Foto { get; set; }

        public string TipoArchivoFoto { get; set; }
    }
}