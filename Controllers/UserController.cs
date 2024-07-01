using fleet_management_backend.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        [Route("Drivers")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> FetchAllDriver()
        {
            try
            {
                var allDrivers = await _userRepository.AllDrivers();

                if(allDrivers.StatusCode == 500)
                {
                    return BadRequest(allDrivers);
                }

                return Ok(allDrivers);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("Fleet-Managers")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> FetchAllFleetManagers()
        {
            try
            {
                var allFleetManagers = await _userRepository.AllFleetManagers();

                if (allFleetManagers.StatusCode == 500)
                {
                    return BadRequest(allFleetManagers);
                }

                return Ok(allFleetManagers);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
