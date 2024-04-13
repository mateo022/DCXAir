using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Services.Journey
{
    public class JourneyService
    {
        private readonly IJsonRepository _jsonRepository;
        public JourneyService(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        private async Task<List<FlightDTO>> getJourney()
        {
            var result = _jsonRepository.GetRoutes().ToList();
            return result;
        }

        private async Task<JourneyDTO> getNewRoute()
        {
            return null;
        }
    }
}
