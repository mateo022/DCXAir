using DCXAirAPI.Application.Cqrs.Currency.Queries;
using DCXAirAPI.Application.Cqrs.Journey.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ApiControllerBase
    {
        /// <summary>
        /// Se realiza consulta de ruta
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCurrency([FromQuery] GetCurrencyQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

    }
}
