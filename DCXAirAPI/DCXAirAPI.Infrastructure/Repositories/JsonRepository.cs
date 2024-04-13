

using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.Repositories;
using Newtonsoft.Json;

namespace DCXAirAPI.Infrastructure.Repositories
{
    public class JsonRepository : IJsonRepository
    {
        private readonly string _jsonFilePath;

        public JsonRepository(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public IEnumerable<FlightDTO> GetRoutes()
        {
            string jsonContent = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<FlightDTO>>(jsonContent);
        }
    }
}
