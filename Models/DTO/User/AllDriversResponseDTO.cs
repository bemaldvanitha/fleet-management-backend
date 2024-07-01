namespace fleet_management_backend.Models.DTO.User
{
    public class AllDriversResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<DriverObject> Drivers { get; set; }
    }

    public class DriverObject
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string DriverLicenceNumber { get; set; }
    }
}
