using DCXAirAPI.Application.DTOs.ResponseFligth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.DTOs.FlightGraph
{
    public class FlightGraph
    {
        public Dictionary<string, List<FlightDTO>> AdjacencyList { get; private set; }

        public FlightGraph(List<FlightDTO> flights)
        {
            AdjacencyList = new Dictionary<string, List<FlightDTO>>();
            foreach (var flight in flights)
            {
                if (!AdjacencyList.ContainsKey(flight.Origin))
                    AdjacencyList[flight.Origin] = new List<FlightDTO>();

                AdjacencyList[flight.Origin].Add(flight);
            }
        }
    }
}
