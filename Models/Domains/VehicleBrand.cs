using System.ComponentModel.DataAnnotations;

namespace fleet_management_backend.Models.Domains
{
    public class VehicleBrand
    {
        public Guid Id { get; set; }

        [Required]
        public string Brand {  get; set; }
    }
}
