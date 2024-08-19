using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    using System;

    public class Evaluacion
    {
        public int IdEvaluacion { get; set; }
        public int IdPersona { get; set; }
        public string Correo { get; set; }
        public string NombreEmpleado { get; set; } // Agregado para el nombre del empleado
        public DateTime Fecha { get; set; }
        public string Objetivos { get; set; } 
        public string Comentarios { get; set; } 
        public int Puntaje { get; set; }
    }

}