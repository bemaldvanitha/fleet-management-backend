using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class TripCertification
    {
        public Guid Id { get; set; }

        [ForeignKey("Trip")]
        public Guid TripId { get; set; }

        [ForeignKey("TripCertificationType")]
        public Guid TripCertificationTypeId { get; set; }

        [Required]
        public string Certification { get; set; }

        public TripCertificationType TripCertificationType { get; set; }
    }
}
