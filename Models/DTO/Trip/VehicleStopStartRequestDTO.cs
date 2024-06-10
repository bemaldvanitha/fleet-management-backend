namespace fleet_management_backend.Models.DTO.Trip
{
    public class VehicleStopStartRequestDTO
    {
        public VehicleStopLocation StopLocation { get; set; }

        public string Reason {  get; set; }
    }

    public class VehicleStopLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Address { get; set; }
    }
}
