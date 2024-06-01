using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class TripStop
    {
        public Guid Id { get; set; }

        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

        [ForeignKey("Trip")]
        public Guid TripId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? Reason { get; set; }

        public Location StopLocation { get; set; }
    }
}
