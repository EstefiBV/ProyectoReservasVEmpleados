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

    public class AmadeusService
    {
        //private readonly HttpClient _httpClient;
        //private readonly string _apiKey = "mQDqL5W7utz4h97ihHcz33ZG4MtyufMm";
        //private readonly string _apiSecret = "ZLScY4187WXy7m5z";
        //private string _accessToken;

        //public AmadeusService()
        //{
        //    _httpClient = new HttpClient();
        //    _httpClient.BaseAddress = new Uri("https://test.api.amadeus.com/");
        //}

        //private async Task<string> GetAccessToken()
        //{
        //    if (_accessToken == null)
        //    {
        //        var request = new HttpRequestMessage(HttpMethod.Post, "v1/security/oauth2/token");
        //        request.Content = new FormUrlEncodedContent(new[]
        //        {
        //        new KeyValuePair<string, string>("grant_type", "client_credentials"),
        //        new KeyValuePair<string, string>("client_id", _apiKey),
        //        new KeyValuePair<string, string>("client_secret", _apiSecret)
        //    });

        //        var response = await _httpClient.SendAsync(request);
        //        response.EnsureSuccessStatusCode();

        //        var content = await response.Content.ReadAsStringAsync();
        //        dynamic jsonResponse = JsonConvert.DeserializeObject(content);
        //        _accessToken = jsonResponse.access_token;
        //    }

        //    return _accessToken;
        //}

        //public async Task<List<FlightDestination>> GetFlightDestinations(string origin, decimal maxPrice)
        //{
        //    var token = await GetAccessToken();

        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    var response = await _httpClient.GetAsync($"v1/shopping/flight-destinations?origin={origin}&maxPrice={maxPrice}");
        //    response.EnsureSuccessStatusCode();

        //    var json = await response.Content.ReadAsStringAsync();
        //    var flightDestinations = JsonConvert.DeserializeObject<List<FlightDestination>>(json);

        //    return flightDestinations;
        //}
    }

}