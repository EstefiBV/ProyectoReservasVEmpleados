using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoHotel.Models
{
    public class Vuelo
    {
        public int IdVuelo { get; set; }
        public string NumeroVuelo { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public DateTime FechaHoraLlegada { get; set; }
        public Aerolinea oAerolinea { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public int NumeroPasajeros { get; set; }  // Nuevo campo agregado
    }
}
