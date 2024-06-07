using fleet_management_backend.Models.DTO.Auth;
using fleet_management_backend.Repositories.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] SignupRequestDTO signupRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               
                SignupResponseDTO signupResponse = await _authRepository.SignUp(signupRequestDTO);

                if(signupResponse.StatusCode == 400 || signupResponse.StatusCode == 500)
                {
                    return BadRequest(signupResponse);
                }

                return Ok(signupResponse);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                LoginResponseDTO loginResponse = await _authRepository.Login(loginRequestDTO);

                if(loginResponse.StatusCode == 401)
                {
                    return Unauthorized(loginResponse);
                }

                if (loginResponse.StatusCode == 500)
                {
                    return BadRequest(loginResponse);
                }

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
