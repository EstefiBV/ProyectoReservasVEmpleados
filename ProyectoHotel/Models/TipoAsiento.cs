using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class TipoAsiento
    {
        public int IdTipoAsiento { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioExtra { get; set; }
        public bool Estado { get; set; }
    }
}