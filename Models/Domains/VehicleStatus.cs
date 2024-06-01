using System.ComponentModel.DataAnnotations;

namespace fleet_management_backend.Models.Domains
{
    public class VehicleStatus
    {
        public Guid Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
