using DCXAirAPI.Application.DTOs.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Interfaces.Currency
{
    public interface ICurrencyService
    {
        Task<double?> ConvertCurrencyAsync(string fromCurrency, string toCurrency, double? amount);

        Task<List<CurrencyDTO>> GetAllowedCurrencies();
    }
}
