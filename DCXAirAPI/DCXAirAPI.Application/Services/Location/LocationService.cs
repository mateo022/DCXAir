using DCXAirAPI.Application.DTOs.Location;
using DCXAirAPI.Application.Interfaces.Location;
using DCXAirAPI.Domain.Enums.Currency;
using DCXAirAPI.Domain.Enums.Location;

namespace DCXAirAPI.Application.Services.Location
{
    public class LocationService: ILocationService
    {
        public async Task<List<LocationDTO>> GetAllowedLocation()
        {
            List<string> allowedCurrencyNames = Enum.GetNames(typeof(LocationEnum)).ToList();

            List<LocationDTO> allowedCurrencies = new List<LocationDTO>();
            foreach (string currencyName in allowedCurrencyNames)
            {
                LocationDTO locationDTO = new LocationDTO();
                locationDTO.NameLocation = currencyName;
                allowedCurrencies.Add(locationDTO);
            }

            return allowedCurrencies;
        }
    }
}
