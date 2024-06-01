using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class DriverVehicle
    {
        public Guid Id { get; set; }

        [ForeignKey("Vehicle")]
        public Guid VehicleId { get; set; }

        [ForeignKey("Driver")]
        public Guid DriverId { get; set; }

        public Vehicle Vehicle { get; set; }

        public Driver Driver { get; set; }
    }
}
