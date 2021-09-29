using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class MaterialesEducativosModel
    {
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Es necesario indicar un titulo para el material")]
        private string Titulo { get; set; }
        
        [Display(Name = "Fecha")]
        private string Fecha { get; set; }

        [Display(Name = "Correo del responsable")]
        [Required(ErrorMessage = "Es necesario indicar le correo del usuario")]
        private string CorreoResponsable { get; set; }

        [Display(Name = "Idioma")]
        private string Idioma { get; set; }
        
        [Display(Name = "Autor")]
        private string Autor { get; set; }

        [Display(Name = "Nombre del responsable")]
        private string NombreResponsable { get; set; }

        [Display(Name = "Imagen de vista previa")]
        public HttpPostedFileBase ImagenVistaPrevia { get; set; }

        public string TipoImagenVistaPrevia { get; set; }

        [Display(Name = "Archivo")]
        [Required(ErrorMessage = "Es necesario subir un archivo")]
        public HttpPostedFileBase Foto { get; set; }

        public string TipoArchivo { get; set; }

    }
}