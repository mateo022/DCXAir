using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.Journey;
using DCXAirAPI.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Services.Journey
{
    public class JourneyService : IJourneyService
    {
        private readonly IJsonRepository _jsonRepository;
        public JourneyService(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        private async Task<List<FlightDTO>> getFlights()
        {
            var result = _jsonRepository.GetRoutes().ToList();
            return result;
        }

        public async Task<JourneyDTO> getJourney(GetRouteQuery getRouteQuery)
        {
            var flight = getFlights();


            var journey = new JourneyDTO();
        
            return journey;

        }
    }
}
