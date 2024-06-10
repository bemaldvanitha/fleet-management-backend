namespace fleet_management_backend.Models.DTO.Fuel
{
    public class AllFuelLevelRecordResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<FuelLevelObj>? FuelLevel { get; set; }
    }

    public class FuelLevelObj
    {
        public string CurrentLevel { get; set; }

        public FuelLevelLocation Location { get; set; }
    }

    public class FuelLevelLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }
    }
}
