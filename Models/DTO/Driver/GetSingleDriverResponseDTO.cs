namespace fleet_management_backend.Models.DTO.Driver
{
    public class GetSingleDriverResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public DriverResponseObj? Driver { get; set; }
    }

    public class SingleDriverObj
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Status { get; set; }

        public string LicenceNumber { get; set; }

        public List<SingleDriverCertificate> Certificates { get; set; }
    }

    public class SingleDriverCertificate
    {
        public string CertificationType { get; set; }

        public string Certificate { get; set; }
    }
}
