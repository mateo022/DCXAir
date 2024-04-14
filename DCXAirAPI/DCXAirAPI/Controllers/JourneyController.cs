using DCXAirAPI.Application.Cqrs.Journey.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ApiControllerBase
    {
        /// <summary>
        /// Se realiza consulta de ruta
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoute([FromQuery] GetRouteQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
