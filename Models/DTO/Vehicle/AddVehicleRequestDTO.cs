namespace fleet_management_backend.Models.DTO.Vehicle
{
    public class AddVehicleRequestDTO
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string RegistrationNumber { get; set; }

        public string VIN { get; set; }

        public DateTime ManufacturedAt { get; set; }

        public DateTime PurchasedAt { get; set; }

        public bool IsBrandNew { get; set; }

        public List<VehicleCertification> VehicleCertifications { get; set; }
    }

    public class VehicleCertification
    {
        public string CertificationType { get; set; }

        public string Certificate { get; set; }
    }
}
