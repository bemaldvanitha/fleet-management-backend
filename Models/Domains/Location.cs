using System.ComponentModel.DataAnnotations;

namespace fleet_management_backend.Models.Domains
{
    public class Location
    {
        public Guid Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
