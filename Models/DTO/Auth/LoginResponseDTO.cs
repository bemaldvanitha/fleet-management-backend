namespace fleet_management_backend.Models.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string UserType { get; set; }
    }
}
