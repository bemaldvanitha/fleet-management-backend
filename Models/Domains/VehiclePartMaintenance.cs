using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class VehiclePartMaintenance
    {
        public Guid Id { get; set; }

        [ForeignKey("VehiclePart")]
        public Guid VehiclePartId { get; set; }

        [ForeignKey("Maintenance")]
        public Guid MaintenanceId { get; set; }

        public int Cost { get; set; }

        public VehiclePart VehiclePart { get; set; }
    }
}
