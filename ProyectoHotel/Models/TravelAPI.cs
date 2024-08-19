using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProyectoHotel.Models
{
    public class TravelAPI
    {
        private readonly string apiKey;
        private readonly string apiSecret;
        private string bearerToken;
        private readonly HttpClient http;

        public TravelAPI()
        {
            var config = ConfigReader.ReadAmadeusAPIConfig();
            apiKey = config.ApiKey;
            apiSecret = config.ApiSecret;
            http = new HttpClient { BaseAddress = new Uri("https://test.api.amadeus.com") };
        }

        public async Task ConnectOAuth()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/v1/security/oauth2/token");
            message.Content = new StringContent(
                $"grant_type=client_credentials&client_id={apiKey}&client_secret={apiSecret}",
                Encoding.UTF8, "application/x-www-form-urlencoded"
            );

            var results = await http.SendAsync(message);
            var stream = await results.Content.ReadAsStringAsync();
            var oauthResults = JsonConvert.DeserializeObject<OAuthResults>(stream);

            bearerToken = oauthResults.access_token;
        }

        private class OAuthResults
        {
            public string access_token { get; set; }
        }

        public async Task<List<object>> ObtenerOpcionesDestino(string origen, decimal maxPrecio)
        {
            var message = new HttpRequestMessage(HttpMethod.Get,
                $"/v1/shopping/flight-destinations?origin={origen}&maxPrice={maxPrecio}");

            ConfigBearerTokenHeader();
            var response = await http.SendAsync(message);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var opciones = new List<object>();
            try
            {
                var jsonObject = JObject.Parse(jsonResponse);
                var data = jsonObject["data"] as JArray;

                if (data == null)
                {
                    throw new Exception("No se encontró la propiedad 'data' en la respuesta JSON.");
                }

                foreach (var item in data)
                {
                    opciones.Add(new
                    {
                        origin = item["origin"]?.ToString(),
                        destination = item["destination"]?.ToString(),
                        price = item["price"]?["total"]?.ToString()  // Ajustar esto según la estructura de la respuesta JSON
                    });
                   
                }
            }
            catch (Exception ex)
            {
                // Registrar la respuesta JSON y el mensaje de error
                System.Diagnostics.Debug.WriteLine("Error al analizar JSON: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Respuesta JSON: " + jsonResponse);
                throw;
            }

            return opciones;
        }



        private void ConfigBearerTokenHeader()
        {
            http.DefaultRequestHeaders.Remove("Authorization");
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
        }
    }
}
