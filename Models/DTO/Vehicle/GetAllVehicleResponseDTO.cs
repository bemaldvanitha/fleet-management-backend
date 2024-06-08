namespace fleet_management_backend.Models.DTO.Vehicle
{
    public class GetAllVehicleResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<VehicleResponseObj> Vehicles { get; set; }
    }

    public class VehicleResponseObj
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public string RegistrationNumber { get; set; }

        public string Status { get; set; }
    }
}
