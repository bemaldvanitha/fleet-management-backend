using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class Fuel
    {
        public Guid Id { get; set; }

        [ForeignKey("Vehicle")]
        public Guid VehicleId { get; set; }

        [Required]
        public int FuelAmount { get; set; }

        [Required]
        public int Cost { get; set; }

        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

        public Location Location { get; set; }
    }
}
