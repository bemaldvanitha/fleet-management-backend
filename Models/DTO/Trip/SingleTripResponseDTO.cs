namespace fleet_management_backend.Models.DTO.Trip
{
    public class SingleTripResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public SingleTripObject? Trip { get; set; }
    }

    public class SingleTripObject
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string VehicleVIN { get; set; }

        public string VehicleDetail { get; set; }

        public Guid VehicleId { get; set; }

        public string DriverName { get; set; }

        public string DriverLicenceNumber { get; set; }

        public Guid DriverId { get; set; }

        public List<TripLocationObj> TripLocations { get; set; }

        public List<TripStopObject> TripStops { get; set; }

        public List<TripCertificationObj> TripCertifications { get; set; }
    }

    public class TripCertificationObject
    {
        public Guid Id { get; set; }

        public string CertificateType { get; set; }

        public string Certificate { get; set; }
    }

    public class TripLocationObject
    {
        public Guid Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }
    }

    public class TripStopObject
    {
        public Guid Id { get; set; }

        public TripLocationObj Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Reason { get; set; }
    }
}
