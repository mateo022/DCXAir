using DCXAirAPI.Application.Interfaces.Currency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Services.Currency
{
    public class CurrencyService: ICurrencyService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public CurrencyService(
             IConfiguration configuration) {
            _configuration = configuration;
            this._httpClient = new HttpClient();
        }

        public async Task<double?> ConvertCurrencyAsync(string fromCurrency, string toCurrency, double? amount)
        {
            // Construye la URL de la API
            var baseUrl = _configuration["API_ConverCurrency"];
            var apiKey = _configuration["API_keyConverCurrency"];
            string url = $"{baseUrl}{apiKey}/pair/{fromCurrency}/{toCurrency}/{amount}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al realizar la solicitud: {response.StatusCode}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();

            var jsonResponse = JsonConvert.DeserializeObject<CurrencyConversionResponse>(responseContent);

            return jsonResponse?.conversion_result;
        }
    }
    public class CurrencyConversionResponse
    {
        [JsonProperty("conversion_result")]
        public double? conversion_result { get; set; }
    }
}
