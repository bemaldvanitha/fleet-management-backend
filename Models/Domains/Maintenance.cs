using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class Maintenance
    {
        public Guid Id { get; set; }

        [ForeignKey("Vehicle")]
        public Guid VehicleId { get; set; }

        [ForeignKey("MaintenanceType")]
        public Guid MaintenanceTypeId { get; set; }

        public int TotalCost { get; set; }

        [Required]
        public Boolean IsRegular { get; set; }

        public MaintenanceType MaintenanceType { get; set; }

        public ICollection<VehiclePartMaintenance> VehiclePartsInMaintenance { get; set; }
    }
}
