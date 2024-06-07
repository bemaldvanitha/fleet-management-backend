namespace fleet_management_backend.Models.DTO.Auth
{
    public class SignupRequestDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string MobileNumber { get; set; }

        public string DisplayName { get; set; }

        public string UserType { get; set; }
    }
}
