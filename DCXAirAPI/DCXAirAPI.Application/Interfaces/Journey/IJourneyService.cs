using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.DTOs.ResponseFligth;


namespace DCXAirAPI.Application.Interfaces.Journey
{
    public interface IJourneyService
    {
        Task<List<JourneyDTO>> getJourney(GetRouteQuery getRouteQuery);
    }
}
