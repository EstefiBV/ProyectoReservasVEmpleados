﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Aerolinea
    {
        public int IdAerolinea { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Estado { get; set; }
    }
}