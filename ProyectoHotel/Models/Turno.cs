using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public int IdPersona { get; set; }
        public string Correo { get; set; }
        public string NombreEmpleado { get; set; } // Agregado para el nombre del empleado
        public string Dias { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}