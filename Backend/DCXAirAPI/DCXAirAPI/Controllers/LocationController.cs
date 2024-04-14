using DCXAirAPI.Application.Cqrs.Currency.Queries;
using DCXAirAPI.Application.Cqrs.Location;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationController : ApiControllerBase
    {
        /// <summary>
        /// Se realiza consulta de ruta
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLocation([FromQuery] GetLocationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

    }
}
