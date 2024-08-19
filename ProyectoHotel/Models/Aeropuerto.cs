using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Aeropuerto
    {
        public int IdAeropuerto { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public bool Estado { get; set; }
    }
}