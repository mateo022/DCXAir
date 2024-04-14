using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.DTOs.FlightGraph;
using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.Currency;
using DCXAirAPI.Application.Interfaces.Journey;
using DCXAirAPI.Application.Interfaces.Repositories;
using DCXAirAPI.Application.Interfaces.RouteFinderBFS;
using Serilog;

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

        private async Task<List<FlightDTO>> GetFlights()
        {
            try
            {
                var result = _jsonRepository.GetRoutes().ToList();
                Log.Information("Vuelos obtenidos correctamente");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("GetFlight => {@ex.Message}", ex);
                throw new Exception("Error al obtener los vuelos.", ex);
            }
        }

        public async Task<List<JourneyDTO>> GetJourney(GetRouteQuery getRouteQuery)
        {
            try
            {
                // Verificar si el origen y destino son iguales
                if (getRouteQuery.Origin == getRouteQuery.Destination)
                {
                    Log.Error("El origen y el destino no pueden ser el mismo lugar.");
                    throw new ArgumentException("El origen y el destino no pueden ser el mismo lugar.");
                }

                var listJourney = new List<JourneyDTO>();

                // Obtén los vuelos de forma asíncrona
                var flights = await GetFlights();

                // Genera el grafo a partir de los vuelos
                var graph = GetGraph(flights);

                // Llama a RouteJourney con await para obtener el resultado de la tarea
                var routeJourney = await RouteJourney(graph, getRouteQuery.Origin, getRouteQuery.Destination, getRouteQuery.Currency);

                // Agrega la ruta al listado de journeys
                listJourney.Add(routeJourney);

                // Si no es un viaje de ida, agrega la ruta de vuelta
                if (!getRouteQuery.IsOneWay)
                {
                    routeJourney = await RouteJourney(graph, getRouteQuery.Destination, getRouteQuery.Origin, getRouteQuery.Currency);
                    listJourney.Add(routeJourney);
                }

                return listJourney;
            }
            catch (ArgumentException ex)
            {
                // Registra solo el mensaje de la excepción
                Log.Error("Error de argumento en getJourney: {Message}. Origen: {Origin}, Destino: {Destination}",
                          ex.Message, getRouteQuery.Origin, getRouteQuery.Destination);
                // Puedes lanzar la excepción o manejarla de acuerdo a tus necesidades
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                // Registra solo el mensaje de la excepción
                Log.Error("Error en getJourney: {Message}.",
                          ex.Message);
                // Puedes lanzar la excepción o manejarla de acuerdo a tus necesidades
                throw new Exception(ex.Message);
            }
        }


        private async Task<JourneyDTO> RouteJourney(Dictionary<string, List<FlightDTO>> graph, string origin, string destination, string currency)
        {
            try
            {
                // Encuentra todas las rutas entre el origen y destino
                var finder = _routeFinderService.FindAllRoutesBFS(graph, origin, destination);

                // Inicializa el precio total del viaje
                double totalPrice = 0;

                // Lista para guardar los vuelos convertidos
                var convertedFlights = new List<FlightDTO>();

                // Itera a través de cada vuelo en el finder
                foreach (var flight in finder)
                {
                    double flightPrice = (double)flight.Price;
                    double convertedPrice = flightPrice;

                    // Solo realiza la conversión de moneda si la moneda seleccionada es diferente de USD
                    if (!string.IsNullOrEmpty(currency) && currency != "USD")
                    {
                        double? convertedAmount = await ConvertPriceAsync(flightPrice, currency);
                        if (convertedAmount.HasValue)
                        {
                            convertedPrice = convertedAmount.Value;
                        }
                    }

                    // Crea un nuevo FlightDTO con el precio convertido
                    var convertedFlight = new FlightDTO
                    {
                        Origin = flight.Origin,
                        Destination = flight.Destination,
                        Price = convertedPrice,
                        Transport = flight.Transport
                    };

                    // Agrega el vuelo convertido a la lista
                    convertedFlights.Add(convertedFlight);

                    // Suma el precio convertido al precio total del viaje
                    totalPrice += convertedPrice;
                }

                // Crea un JourneyDTO con los datos obtenidos
                var journey = new JourneyDTO
                {
                    Origin = origin,
                    Destination = destination,
                    Price = totalPrice,
                    Flights = convertedFlights
                };

                return journey;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el precio del recorrido.", ex);
            }
        }

        // Función separada para convertir el precio de una moneda a otra
        private async Task<double?> ConvertPriceAsync(double amount, string targetCurrency)
        {
            // Llama al servicio de conversión de moneda solo si la moneda objetivo es diferente de USD
            Log.Information("LA MONEDA ES DIFERENTE USD");
            if (!string.IsNullOrEmpty(targetCurrency) && targetCurrency != "USD")
            {
                return await _currencyService.ConvertCurrencyAsync("USD", targetCurrency, amount);
            }
            // Si la moneda objetivo es USD, no se realiza conversión y se retorna el mismo monto
            return amount;
        }
        private Dictionary<string, List<FlightDTO>> GetGraph(List<FlightDTO> flightDTO)
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
