using DCXAirAPI.Application.DTOs.ResponseFligth;
using DCXAirAPI.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJsonRepository _jsonRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJsonRepository jsonRepository)
        {
            _logger = logger;
            _jsonRepository = jsonRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<ResponseFlightDTO> Get()
        {
            var resultJson = _jsonRepository.GetRoutes();

            return resultJson;
        }
    }
}
