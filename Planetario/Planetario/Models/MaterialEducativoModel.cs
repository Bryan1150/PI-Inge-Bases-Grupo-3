using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class MaterialEducativoModel
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Es necesario indicar un titulo para el material")]
        public string Titulo { get; set; }
        
        [Display(Name = "Fecha")]
        public string Fecha { get; set; }

        [Display(Name = "Correo del responsable")]
        [Required(ErrorMessage = "Es necesario indicar le correo del usuario")]
        public string CorreoResponsable { get; set; }

        [Display(Name = "Publico dirigido")]
        public string PublicoDirigido { get; set; }

        [Display(Name = "Nombre del responsable")]
        public string NombreResponsable { get; set; }

        [Display(Name = "Imagen de vista previa")]
        public HttpPostedFileBase ImagenVistaPrevia { get; set; }

        public string TipoImagenVistaPrevia { get; set; }

        [Display(Name = "Archivo")]
        [Required(ErrorMessage = "Es necesario subir un archivo")]
        public HttpPostedFileBase Archivo { get; set; }

        public string TipoArchivo { get; set; }

        public bool HayVistaPrevia()
        {
            return ImagenVistaPrevia != null;
        }

    }
}