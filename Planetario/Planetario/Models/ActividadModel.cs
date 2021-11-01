﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Planetario.Models
{
    public class ActividadModel
    {
        [Required(ErrorMessage = "Es necesario que le indique el nombre que va a tener la actividad.")]
        [Display(Name = "Nombre de la actividad:")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string NombreActividad { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la descripcion de la actividad.")]
        [Display(Name = "Descripcion de la actividad:")]
        [MaxLength(100, ErrorMessage = "Se tiene un máximo de 100 cáracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese aproximadamente cuantos minutos durara la actividad.")]
        [Display(Name = "Duración")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar números")]
        public int Duracion { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese que tan compleja es la actividad.")]
        [Display(Name = "Complejidad")]
        public string Complejidad { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese aproximadamente cuantos colones cuesta la actividad.")]
        [Display(Name = "Precio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar números")]
        public double PrecioAproximado { get; set; }

        [Required(ErrorMessage = "Es necesario que indique la categoria de la actividad.")]
        [Display(Name = "Categoria")]
        
        public string Categoria { get; set; }
        
        [Required(ErrorMessage = "Es necesario que indique el dia de la semana.")]
        [Display(Name = "Dia de la semana")]
        public string DiaSemana { get; set; }

        [Required(ErrorMessage = "Es necesario que indique su correo.")]
        [Display(Name = "Correo")]
        public string PropuestoPor { get; set; }

        [Display(Name = "Display")]
        public bool Aprobado { get; set; }

        [Required(ErrorMessage = "Es necesario que indique el publico al que va dirigido la actividad.")]
        [Display(Name = "Correo")]
        public string PublicoDirigido { get; set; }

    }
}