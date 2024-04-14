using DCXAirAPI.Application.DTOs.Journey;
using DCXAirAPI.Application.Interfaces.Journey;
using MediatR;

namespace DCXAirAPI.Application.Cqrs.Journey.Queries
{
    public class GetRouteQuery : IRequest<List<JourneyDTO>>
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool IsOneWay { get; set; }
        public string Currency { get; set; }
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
            return await _journeyService.GetJourney(request);
        }
    }
}
