using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Planetario.Models
{
    public class FuncionarioModel
    {
        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "Es necesario que ingrese una cedula")]
        public int cedula { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es necesario que ingrese una nombre")]
        public string nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "Es necesario que ingrese al menos un apellido")]
        public string apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string apellido2 { get; set; }

        [Display(Name = "Fecha de incorporación")]
        [Required(ErrorMessage = "Es necesario que ingrese la fecha en la que empezó a trabajar el funcionario")]
        public string fechaIncorporacion { get; set; }

        [Display(Name = "Titulo")]
        public string titulo { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Es necesario que ingrese el rol del funcionario")]
        public string rolString { get; set; }

        [Display(Name = "Correo")]
        public string correoContacto { get; set; }

        [Display(Name = "Foto del funcionario")]
        public HttpPostedFileBase foto { get; set; }

        public string tipoArchivoFoto { get; set; }

    }
}