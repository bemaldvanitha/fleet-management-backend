namespace fleet_management_backend.Models.DTO.Vehicle
{
    public class GetLastLocationResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public LastLocationObj? LastLocation { get; set; }
    }

    public class LastLocationObj
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }
    }
}
