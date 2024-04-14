using DCXAirAPI.Application.DTOs.FlightGraph;
using DCXAirAPI.Application.DTOs.ResponseFligth;

namespace DCXAirAPI.Application.Interfaces.RouteFinderBFS
{
    public interface IRouteFinderService
    {
        List<FlightDTO> FindAllRoutesBFS(Dictionary<string, List<FlightDTO>> graph, string start, string end);
    }
}
