using DCXAirAPI.Application.DTOs.ResponseTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.DTOs.ResponseFligth
{
    public class FlightDTO
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double? Price { get; set; }
        public TransportDTO Transport { get; set; }
    }
}
