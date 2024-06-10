namespace fleet_management_backend.Models.DTO.Fuel
{
    public class FuelLevelRecordRequestDTO
    {
        public Guid VehicleId { get; set; }

        public string CurrentLevel { get; set; }

        public CurrectLevelLocation Location { get; set; }
    }

    public class CurrectLevelLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }
    }
}
