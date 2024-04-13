using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.DTOs.Journey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Interfaces.Journey
{
    public interface IJourneyService
    {
        Task<JourneyDTO> GetRoute(GetRouteQuery getRouteQuery);
    }
}
