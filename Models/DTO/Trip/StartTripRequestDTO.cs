namespace fleet_management_backend.Models.DTO.Trip
{
    public class StartTripRequestDTO
    {
        public Guid DriverId { get; set; }

        public Guid VehicleId { get; set; }

        public TripLocationObj? StartLocation { get; set; }

        public TripLocationObj? EndLocation { get; set; }

        public List<TripCertificationObj> Certifications { get; set; }
    }

    public class TripLocationObj
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class TripCertificationObj
    {
        public string CertificateType { get; set; }

        public string Certificate { get; set; }
    }
}
