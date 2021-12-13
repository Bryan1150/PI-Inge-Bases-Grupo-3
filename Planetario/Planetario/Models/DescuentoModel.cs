using System.ComponentModel.DataAnnotations;

namespace Planetario.Models
{
    public class DescuentoModel
    {
        [Required(ErrorMessage = "Es necesario que indique el codigo de descuento")]
        [Display(Name = "Código")]
        [MaxLength(10, ErrorMessage = "Se tiene un máximo de 10 cáracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el porcentaje de descuento")]
        [Display(Name = "Porcentaje")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar números")]
        public int Descuento { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el tipo de membresía")]
        [Display(Name = "Membresía")]
        public string Membresia { get; set; }
    }
}