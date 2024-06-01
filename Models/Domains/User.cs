using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleet_management_backend.Models.Domains
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Password { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string MobileNumber { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [ForeignKey("UserType")]
        public Guid UserTypeId { get; set; }

        public UserType UserType { get; set; }
    }
}
