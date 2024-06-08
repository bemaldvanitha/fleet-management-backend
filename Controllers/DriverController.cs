using fleet_management_backend.Models.DTO.Driver;
using fleet_management_backend.Repositories.Drivers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;

        public DriverController(IDriverRepository driverRepository)
        {
            this._driverRepository = driverRepository;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateDriverProfile([FromBody] AddDriverRequestDTO addDriverRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                DriverResponseDTO driverResponse = await _driverRepository.AddDriver(addDriverRequest);

                if(driverResponse.StatusCode == 500)
                {
                    return BadRequest(driverResponse);
                }

                return Ok(driverResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
