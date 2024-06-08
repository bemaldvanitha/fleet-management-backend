namespace fleet_management_backend.Models.DTO.Vehicle
{
    public class GetSingleVehicleResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public SingleVehicleResponse? Vehicle { get; set; }
    }

    public class SingleVehicleResponse
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Status { get; set; }

        public string RegistrationNumber { get; set; }

        public string VIN { get; set; }

        public DateTime? ManufacturedAt { get; set; }

        public DateTime? PurchasedAt { get; set; }

        public bool IsBrandNew { get; set; }

        public List<SingleVehicleCertificate> VehicleCertificates { get; set; }
    }

    public class SingleVehicleCertificate
    {
        public string Certificate { get; set; }

        public string CertificateType { get; set; }
    }
}
