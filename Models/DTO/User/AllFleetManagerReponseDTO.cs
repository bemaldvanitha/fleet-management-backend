namespace fleet_management_backend.Models.DTO.User
{
    public class AllFleetManagerReponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<FleetManagerObject>? FleetManagers { get; set; }
    }

    public class FleetManagerObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }
    }
}
