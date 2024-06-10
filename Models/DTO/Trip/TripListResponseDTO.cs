namespace fleet_management_backend.Models.DTO.Trip
{
    public class TripListResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<TripListObject> Trips { get; set; }
    }

    public class TripListObject
    {
        public Guid Id { get; set; }

        public string DriverName { get; set; }

        public string VehicleVIN { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

        public string TripStatus { get; set; }
    }
}
