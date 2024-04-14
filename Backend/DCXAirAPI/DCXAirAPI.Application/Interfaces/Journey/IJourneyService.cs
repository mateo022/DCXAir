using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.DTOs.Journey;



namespace DCXAirAPI.Application.Interfaces.Journey
{
    public interface IJourneyService
    {
        Task<List<JourneyDTO>> GetJourney(GetRouteQuery getRouteQuery);
    }
}
