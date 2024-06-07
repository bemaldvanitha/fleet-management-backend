namespace fleet_management_backend.Models.DTO.Auth
{
    public class UserProfileResponseDTO
    {
        public UserProfile? UserProfile { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }

    public class UserProfile
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string UserType { get; set; }
    }
}
