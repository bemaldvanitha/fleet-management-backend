using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class Trip
    {
        public Guid Id { get; set; }

        [ForeignKey("Location")]
        public Guid StartLocationId { get; set; }

        [ForeignKey("Location")]
        public Guid EndLocationId { get; set;}

        [ForeignKey("Vehicle")]
        public Guid VehicleId { get; set; }

        [ForeignKey("Driver")]
        public Guid DriverId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set;}

        public Vehicle Vehicle { get; set; }

        public Driver Driver { get; set; }

        public Location StartLocation { get; set; }

        public Location EndLocation { get; set; }

        public ICollection<TripStop> TripStops { get; set; }

        public ICollection<TripLocation> TripLocations { get; set; }
    }
}
