namespace fleet_management_backend.Models.DTO.Trip
{
    public class TripLocationRequestDTO
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Address { get; set; }
    }
}
