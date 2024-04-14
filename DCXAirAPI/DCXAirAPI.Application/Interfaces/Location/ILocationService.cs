using DCXAirAPI.Application.DTOs.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Application.Interfaces.Location
{
    public interface ILocationService
    {
        Task<List<LocationDTO>> GetAllowedLocation();
    }
}
