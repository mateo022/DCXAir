using DCXAirAPI.Application.DTOs.ResponseFligth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Services.Journey
{
    public class JourneyService
    {
        public JourneyService()
        {
            
        }

        private async Task<List<ResponseFlightDTO>> getJourney()
        {
            string filePath = "./markets.json";
            string jsonData = File.ReadAllText(filePath);
            return null;
        }
    }
}
