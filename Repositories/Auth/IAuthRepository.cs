using fleet_management_backend.Models.DTO.Auth;

namespace fleet_management_backend.Repositories.Auth
{
    public interface IAuthRepository
    {
        public Task<SignupResponseDTO> SignUp(SignupRequestDTO signupRequest);

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
    }
}
