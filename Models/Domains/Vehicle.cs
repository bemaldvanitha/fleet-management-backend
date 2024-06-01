using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        [ForeignKey("VehicleBrand")]
        public Guid VehicleBrandId { get; set; }

        [ForeignKey("VehicleModel")]
        public Guid VehicleModelId { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string VIN { get; set; }

        public DateTime? ManufacturedAt { get; set; }

        public DateTime? PurchasedAt { get; set; }

        [Required]
        public Boolean IsActive { get; set; }

        [ForeignKey("VehicleStatus")]
        public Guid VehicleStatusId { get; set; }

        public VehicleBrand VehicleBrand { get; set; }

        public VehicleModel VehicleModel { get; set; }

        public VehicleStatus VehicleStatus { get; set; }

        public ICollection<VehicleCertification> VehicleCertifications { get; set; }
    }
}
