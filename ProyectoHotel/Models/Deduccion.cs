using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Deduccion
    {
        public int IdDeduccion { get; set; }
        public string Descripcion { get; set; } 
        public decimal Porcentaje { get; set; } 

    }
}