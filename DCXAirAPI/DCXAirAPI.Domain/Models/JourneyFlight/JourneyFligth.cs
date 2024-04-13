using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Domain.Models.JourneyFlight
{
    public class JourneyFligth: Entity.Entity
    {
        public Guid FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight.Flight? Flight { get; set; }
        public Guid JourneyId { get; set; }
        [ForeignKey("JourneyId")]
        public Journey.Journey? Journey { get; set; }
    }
}
