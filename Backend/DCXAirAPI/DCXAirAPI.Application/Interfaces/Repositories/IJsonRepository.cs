using DCXAirAPI.Application.DTOs.ResponseFligth;

namespace DCXAirAPI.Application.Interfaces.Repositories
{
    public interface IJsonRepository
    {
        IEnumerable<FlightDTO> GetRoutes();
    }
}