using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProyectoHotel.Models
{
    public class AmadeusAPIConfig
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

}
