using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public int IdPersona { get; set; }
        public string Correo { get; set; }
        public string NombreEmpleado { get; set; } // Agregado para el nombre del empleado
        public string PermisoDescripcion { get; set; } 
    }
}