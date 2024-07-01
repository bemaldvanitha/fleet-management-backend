namespace fleet_management_backend.Models.DTO.Driver
{
    public class FetchDriverIdResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public Guid? DriverId { get; set; }
    }
}
