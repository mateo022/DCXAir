using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.Interfaces.Journey;
using DCXAirAPI.Application.Services.Journey;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Cqrs.Journey.Queries
{
    public class GetRouteQuery : IRequest<List<JourneyDTO>>
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool IsOneWay { get; set; }
        
    }

    public class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, List<JourneyDTO>>
    {
        private readonly IJourneyService _journeyService;
        public GetRouteQueryHandler(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        public async Task<List<JourneyDTO>> Handle(GetRouteQuery request, CancellationToken cancellationToken)
        {
            return await _journeyService.getJourney(request);
        }
    }
}
