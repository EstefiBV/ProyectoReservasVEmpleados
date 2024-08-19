using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class ReservaVuelo
    {
        public int IdReserva { get; set; }
        public Persona oPersona { get; set; }
        public Vuelo oVuelo { get; set; }
        public Equipaje oEquipaje { get; set; }
        public TipoAsiento oTipoAsiento { get; set; }
        public decimal PrecioVuelo { get; set; }
        public decimal? PrecioAsiento { get; set; }
        public decimal? PrecioEquipaje { get; set; }
        public decimal PrecioTotal { get; set; }
        public bool Estado { get; set; }
        public int CantidadPasajeros { get; set; } // Nueva propiedad para cantidad de pasajeros
    }
}
