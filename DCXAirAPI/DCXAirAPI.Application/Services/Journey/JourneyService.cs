using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.DTOs.FlightGraph;
using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.Currency;
using DCXAirAPI.Application.Interfaces.Journey;
using DCXAirAPI.Application.Interfaces.Repositories;
using DCXAirAPI.Application.Interfaces.RouteFinderBFS;

namespace DCXAirAPI.Application.Services.Journey
{
    public class JourneyService : IJourneyService
    {
        private readonly IJsonRepository _jsonRepository;

        private readonly IRouteFinderService _routeFinderService;
        private readonly ICurrencyService _currencyService;
        public JourneyService(
            IJsonRepository jsonRepository,
            IRouteFinderService routeFinderService,
            ICurrencyService currencyService)
        {
            _jsonRepository = jsonRepository;
            _routeFinderService = routeFinderService;
            _currencyService = currencyService;
        }

        private async Task<List<FlightDTO>> getFlights()
        {
            try
            {
                var result = _jsonRepository.GetRoutes().ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los vuelos.", ex);
            }
        }

        public async Task<List<JourneyDTO>> getJourney(GetRouteQuery getRouteQuery)
        {
            try
            {
                var listJourney = new List<JourneyDTO>();

                var flights = await getFlights();

                var graph = getGraph(flights);

                var routeJourney = await RouteJourney(graph, getRouteQuery.Origin, getRouteQuery.Destination, getRouteQuery.Currency);

                listJourney.Add(routeJourney);

                if (!getRouteQuery.IsOneWay)
                {
                    routeJourney = await RouteJourney(graph, getRouteQuery.Destination, getRouteQuery.Origin, getRouteQuery.Currency);
                    listJourney.Add(routeJourney);
                }

                return listJourney;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el recorrido seleccionado.", ex);
            }
        }


        private async Task<JourneyDTO> RouteJourney(Dictionary<string, List<FlightDTO>> graph, string origin, string destination, string currency)
        {
            try
            {
                var finder = _routeFinderService.FindAllRoutesBFS(graph, origin, destination);

                double priceJourney = 0;

                var convertedFlights = new List<FlightDTO>();

                foreach (var flight in finder)
                {
                    double convertedPrice = (double)flight.Price;
                    if (!string.IsNullOrEmpty(currency) && currency != "USD")
                    {
                        double? convertedAmount = await _currencyService.ConvertCurrencyAsync("USD", currency, flight.Price);
                        if (convertedAmount.HasValue)
                        {
                            convertedPrice = convertedAmount.Value;
                        }
                    }

                    var convertedFlight = new FlightDTO
                    {
                        Origin = flight.Origin,
                        Destination = flight.Destination,
                        Price = convertedPrice,
                        Transport = flight.Transport
                    };
                    convertedFlights.Add(convertedFlight);
                    priceJourney += convertedPrice;
                }

                var journey = new JourneyDTO
                {
                    Origin = origin,
                    Destination = destination,
                    Price = priceJourney,
                    Flights = convertedFlights
                };

                return journey;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el precio del recorrido.", ex);
            }
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
