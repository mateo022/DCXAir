using DCXAirAPI.Application.DTOs.Currency;

namespace DCXAirAPI.Application.Interfaces.Currency
{
    public interface ICurrencyService
    {
        Task<double?> ConvertCurrencyAsync(string fromCurrency, string toCurrency, double? amount);

        Task<List<CurrencyDTO>> GetAllowedCurrencies();
    }
}
