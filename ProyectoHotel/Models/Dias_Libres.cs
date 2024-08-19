using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{

    public class DiaLibre
    {
        public int IdDiaLibre { get; set; }
        public int IdPersona { get; set; }
        public string Correo { get; set; }
        public string NombreEmpleado { get; set; } // Agregado para el nombre del empleado
        public DateTime Fecha { get; set; }
        public string TipoDiaLibre { get; set; }
        public string Motivo { get; set; } 
    }

}