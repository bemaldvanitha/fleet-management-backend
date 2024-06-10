namespace fleet_management_backend.Models.DTO.Fuel
{
    public class FuelRefillRequestDTO
    {
        public Guid VehicleId { get; set; }

        public int FuelAmount { get; set; }

        public int Cost { get; set; }

        public string StartFuelLevel { get; set; }

        public string EndFuelLevel { get; set;  }

        public FuelRefillLocation FuelRefillLocation { get; set; }
    }

    public class FuelRefillLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }
    }
}
