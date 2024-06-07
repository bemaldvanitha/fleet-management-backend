namespace fleet_management_backend.Models.DTO.Auth
{
    public class SignupResponseDTO
    {
        public string Token { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
