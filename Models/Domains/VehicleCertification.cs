using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class VehicleCertification
    {
        public Guid Id { get; set; }

        [ForeignKey("Vehicle")]
        public Guid VehicleId { get; set; }

        [ForeignKey("VehicleCertificationType")]
        public Guid VehicleCertificationTypeId { get; set; }

        public string Certification { get; set; }

        public VehicleCertificationType CertificationType { get; set; }
    }
}
