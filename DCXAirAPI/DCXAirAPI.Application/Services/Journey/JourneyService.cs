using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.DTOs.FlightGraph;
using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.Journey;
using DCXAirAPI.Application.Interfaces.Repositories;
using DCXAirAPI.Application.Interfaces.RouteFinderBFS;

namespace DCXAirAPI.Application.Services.Journey
{
    public class JourneyService : IJourneyService
    {
        private readonly IJsonRepository _jsonRepository;

        private readonly IRouteFinderService _routeFinderService;
        public JourneyService(
            IJsonRepository jsonRepository,
            IRouteFinderService routeFinderService)
        {
            _jsonRepository = jsonRepository;
            _routeFinderService = routeFinderService;
        }

        private async Task<List<FlightDTO>> getFlights()
        {
            var result = _jsonRepository.GetRoutes().ToList();
            return result;
        }

        public async Task<JourneyDTO> getJourney(GetRouteQuery getRouteQuery)
        {

            var journey = new JourneyDTO();
            var flights = getFlights();

            var graph = getGraph(flights.Result);

            var finder = _routeFinderService.FindAllRoutesBFS(graph, getRouteQuery.Origin, getRouteQuery.Destination);

            var priceJourney = finder.Select(x => x.price).Sum();

            journey = new JourneyDTO
            {
                Origin = getRouteQuery.Origin,
                Destination = getRouteQuery.Destination,
                Price = priceJourney,
                Flights = finder
            };

            return journey;

        }

        private Dictionary<string, List<FlightDTO>> getGraph(List<FlightDTO> flightDTO)
        {
            var graph = new Dictionary<string, List<FlightDTO>>();

            foreach (var flight in flightDTO)
            {
                if (!graph.ContainsKey(flight.Origin))
                {
                    graph[flight.Origin] = new List<FlightDTO>();
                }
                graph[flight.Origin].Add(flight);
            }
            return graph;
        }
    }

     
}
