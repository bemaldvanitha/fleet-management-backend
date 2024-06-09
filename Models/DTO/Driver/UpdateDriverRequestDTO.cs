namespace fleet_management_backend.Models.DTO.Driver
{
    public class UpdateDriverRequestDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LicenceNumber { get; set; }

        public List<VehicleCertificationObject> VehicleCertifications { get; set; }
    }

    public class  VehicleCertificationObject
    {
        public string Certificate { get; set; }

        public string CertificateType { get; set; }
    }
}
