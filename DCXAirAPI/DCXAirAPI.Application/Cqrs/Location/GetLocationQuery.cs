using DCXAirAPI.Application.Cqrs.Currency.Queries;
using DCXAirAPI.Application.DTOs.Currency;
using DCXAirAPI.Application.DTOs.Location;
using DCXAirAPI.Application.Interfaces.Currency;
using DCXAirAPI.Application.Interfaces.Location;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Cqrs.Location
{
    public class GetLocationQuery : IRequest<List<LocationDTO>>
    {
    }

    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, List<LocationDTO>>
    {
        private readonly ILocationService _locationService;
        public GetLocationQueryHandler(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<LocationDTO>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            return await _locationService.GetAllowedLocation();
        }
    }
}
