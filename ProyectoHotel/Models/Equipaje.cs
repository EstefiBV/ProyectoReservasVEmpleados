using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Equipaje
    {
        public int IdEquipaje { get; set; }
        public decimal Peso { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
    }

}