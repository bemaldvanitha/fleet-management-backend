namespace fleet_management_backend.Models.DTO.Vehicle
{
    public class UpdateVehicleRequestDTO
    {
        public List<VehicleCertificateObject> VehicleCertificates { get; set; }
    }

    public class VehicleCertificateObject
    {
        public string Certificate { get; set; }

        public string CertificateType { get; set; }
    }
}
