using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class Driver
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string LicenceNumber { get; set; }

        [ForeignKey("DriverStatus")]
        public Guid DriverStatusId { get; set; }

        public User User { get; set; }

        public DriverStatus DriverStatus { get; set; }

        public ICollection<DriverCertification> DriverCertifications { get; set; }
    }
}
