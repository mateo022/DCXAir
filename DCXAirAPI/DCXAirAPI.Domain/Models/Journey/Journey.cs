namespace DCXAirAPI.Domain.Models.Journey
{
    public class Journey : Entity.Entity
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Price { get; set; }
        public List<Flight.Flight> Flights { get; set; }

    }
}
