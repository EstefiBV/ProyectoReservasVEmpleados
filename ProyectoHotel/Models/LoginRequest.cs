using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}