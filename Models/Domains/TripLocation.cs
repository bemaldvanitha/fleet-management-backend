using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class TripLocation
    {
        public Guid Id { get; set; }

        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

        [ForeignKey("Trip")]
        public Guid TripId { get; set; }

        public Location Location { get; set; }
    }
}
