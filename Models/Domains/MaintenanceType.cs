using System.ComponentModel.DataAnnotations;

namespace fleet_management_backend.Models.Domains
{
    public class MaintenanceType
    {
        public Guid Id { get; set; }

        [Required]
        public string Type {  get; set; }
    }
}
