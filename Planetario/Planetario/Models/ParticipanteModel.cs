using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Planetario.Models
{
    public class ParticipanteModel
    {
        public string Correo { get; set; }

        public string Nombre { get; set; }
        
        public string Apellido1 { get; set; }
        
        public string Apellido2 { get; set; }
        
        public string Genero { get; set; }
        
        public string Pais { get; set; }
        
        public string FechaNacimiento { get; set; }
        
        public string NivelEducativo { get; set; }
        
        public string NombreActividad { get; set; }
    }
}