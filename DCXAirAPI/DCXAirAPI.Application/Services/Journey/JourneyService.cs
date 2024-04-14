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

        public async Task<List<JourneyDTO>> getJourney(GetRouteQuery getRouteQuery)
        {

            var ListJourney = new List<JourneyDTO>();
            var flights = getFlights();

            var graph = getGraph(flights.Result);

            var routeJourney = RouteJourney(graph, getRouteQuery.Origin, getRouteQuery.Destination);

            ListJourney.Add(routeJourney);

            if (!getRouteQuery.IsOneWay)
            {
                routeJourney = RouteJourney(graph, getRouteQuery.Destination, getRouteQuery.Origin);

                ListJourney.Add(routeJourney);
            }

            return ListJourney;

        }


        private JourneyDTO RouteJourney(Dictionary<string, List<FlightDTO>> graph, string Origin, string Destination)
        {
            var finder = _routeFinderService.FindAllRoutesBFS(graph, Origin, Destination);

            var priceJourney = finder.Select(x => x.price).Sum();

            var journey = new JourneyDTO
            {
                Origin = Origin,
                Destination = Destination,
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
