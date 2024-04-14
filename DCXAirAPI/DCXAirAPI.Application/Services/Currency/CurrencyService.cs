using DCXAirAPI.Application.DTOs.Currency;
using DCXAirAPI.Application.Interfaces.Currency;
using DCXAirAPI.Domain.Enums.Currency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DCXAirAPI.Application.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public CurrencyService(
             IConfiguration configuration)
        {
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
        public async Task<List<CurrencyDTO>> GetAllowedCurrencies()
        {
    
            List<string> allowedCurrencyNames = Enum.GetNames(typeof(CurrencyEnum)).ToList();

            List<CurrencyDTO> allowedCurrencies = new List<CurrencyDTO>();
            foreach (string currencyName in allowedCurrencyNames)
            {
                CurrencyDTO currencyDTO = new CurrencyDTO();
                currencyDTO.NameCurrency = currencyName;

                allowedCurrencies.Add(currencyDTO);
            }

            return allowedCurrencies;
        }
        public class CurrencyConversionResponse
        {
            [JsonProperty("conversion_result")]
            public double? conversion_result { get; set; }
        }
    }

}
