using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class DriverCertification
    {
        public Guid Id { get; set; }

        [ForeignKey("Driver")]
        public Guid DriverId { get; set; }

        [ForeignKey("DriverCertificationType")]
        public Guid DriverCertificationTypeId { get; set; }

        [Required]
        public string Certification { get; set; }

        public DriverCertificationType CertificationType { get; set; }
    }
}
