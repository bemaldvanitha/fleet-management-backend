namespace fleet_management_backend.Models.DTO.Fuel
{
    public class AllFuelRefillResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<FuelRefillObject>? FuelRefills { get; set;}
    }

    public class FuelRefillObject
    {
        public int FuelAmount { get; set; }

        public int Cost { get; set; }

        public FuelRefillLocationObject Location { get; set; }
    }

    public class FuelRefillLocationObject
    {
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
