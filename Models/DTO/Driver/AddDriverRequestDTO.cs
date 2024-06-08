namespace fleet_management_backend.Models.DTO.Driver
{
    public class AddDriverRequestDTO
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LicenceNumber { get; set; }

        public List<DriverCertificationItem> DriverCertifications { get; set; }
    }

    public class DriverCertificationItem
    {
        public string CertificationType { get; set; }

        public string Certificate { get; set; }
    }
}
