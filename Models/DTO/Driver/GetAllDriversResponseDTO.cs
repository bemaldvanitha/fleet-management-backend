namespace fleet_management_backend.Models.DTO.Driver
{
    public class GetAllDriversResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<DriverResponseObj>? Drivers { get; set; }
    }

    public class DriverResponseObj
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LicenceNumber { get; set; }

        public string Status { get; set; }

        public string MobileNumber { get; set; }
    }
}
