using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Reunion
    {
        public int IdReunion { get; set; }
        public int IdPersona { get; set; }
        public string Correo { get; set; }
        public string NombreEmpleado { get; set; } // Agregado para el nombre del empleado
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; } 
    }
}