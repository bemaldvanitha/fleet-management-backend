using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class VehicleFuelLevel
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }

        [Required]
        public string CurrentLevel { get; set; }

        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

        public Location FuelLocation { get; set; }
    }
}
