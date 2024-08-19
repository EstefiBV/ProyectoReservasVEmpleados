using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Nomina
    {
        public int IdNomina { get; set; }
        public int IdPersona { get; set; }
        public string Correo { get; set; }
        public string NombreEmpleado { get; set; } // Agregado para el nombre del empleado
        public string NombreRol { get; set; } // Agregado para el nombre del Rol
        public DateTime Fecha { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal Deducciones { get; set; }
        public decimal Bonificaciones { get; set; }
        public decimal SalarioNeto { get; set; }
       

    }
}