using DCXAirAPI.Application.DTOs.FlightGraph;
using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.RouteFinderBFS;

namespace DCXAirAPI.Application.Services.RouteFinderBFS
{
    public class RouteFinderService: IRouteFinderService
    {
        public List<FlightDTO> FindAllRoutesBFS(Dictionary<string, List<FlightDTO>> graph, string start, string end)
        {
            // Cola para las rutas de vuelo
            var queue = new Queue<List<FlightDTO>>();
            // Conjunto de aeropuertos visitados
            var visited = new HashSet<string>();

            // Iniciar la búsqueda con una lista de vuelos vacía desde el aeropuerto de origen
            queue.Enqueue(new List<FlightDTO>());
            visited.Add(start);

            while (queue.Count > 0)
            {
                // Obtener la ruta actual de la cola
                var currentRoute = queue.Dequeue();
                var currentNode = currentRoute.Count > 0 ? currentRoute[^1].Destination : start;

                // Si hemos llegado al destino
                if (currentNode == end)
                {
                    return currentRoute;
                }

                // Explorar los vuelos disponibles desde el aeropuerto actual
                if (graph.ContainsKey(currentNode))
                {
                    foreach (var flight in graph[currentNode])
                    {
                        if (!visited.Contains(flight.Destination))
                        {
                            // Marcar el destino como visitado
                            visited.Add(flight.Destination);
                            // Crear una nueva ruta con el vuelo actual
                            var newRoute = new List<FlightDTO>(currentRoute) { flight };
                            // Encolar la nueva ruta
                            queue.Enqueue(newRoute);
                        }
                    }
                }
            }

            // Si no se encontró una ruta al destino
            return null;
        }
    }
}
