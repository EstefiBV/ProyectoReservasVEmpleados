using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Bonificacion
    {
        public int IdBonificacion { get; set; }
        public int IdRol { get; set; }
        public string Descripcion { get; set; } 
        public decimal Monto { get; set; } 
    }
}