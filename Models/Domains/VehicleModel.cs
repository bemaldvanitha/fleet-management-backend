using System.ComponentModel.DataAnnotations;

namespace fleet_management_backend.Models.Domains
{
    public class VehicleModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Model { get; set; }
    }
}
