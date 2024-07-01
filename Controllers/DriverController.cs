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

        [HttpGet]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetAllDrivers()
        {
            try
            {
                GetAllDriversResponseDTO getAllDrivers = await _driverRepository.GetAllDrivers();

                if(getAllDrivers.StatusCode == 500)
                {
                    return BadRequest(getAllDrivers);
                }

                return Ok(getAllDrivers);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("Available")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetAvailableDrivers()
        {
            try
            {
                var getAvailableDrivers = await _driverRepository.GetAvailableDrivers();

                if(getAvailableDrivers.StatusCode == 500)
                {
                    return BadRequest(getAvailableDrivers);
                }

                return Ok(getAvailableDrivers);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("{vehicle_id}")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetSingleDriver([FromRoute] string vehicle_id)
        {
            try
            {
                var singleDriver = await _driverRepository.GetSingleDriver(Guid.Parse(vehicle_id));

                if(singleDriver.StatusCode == 500)
                {
                    return BadRequest(singleDriver);
                }

                if(singleDriver.StatusCode == 404)
                {
                    return NotFound(singleDriver);
                }

                return Ok(singleDriver);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("Status/{vehicle_id}")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> UpdateDriverStatus([FromRoute] string vehicle_id)
        {
            try
            {
                var updateDriverStatus = await _driverRepository.ChangeDriverStatus(Guid.Parse(vehicle_id));

                if(updateDriverStatus.StatusCode == 500)
                {
                    return BadRequest(updateDriverStatus);
                }

                if(updateDriverStatus.StatusCode == 404)
                {
                    return NotFound(updateDriverStatus);
                }

                return Ok(updateDriverStatus);
            }catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpDelete]
        [Route("Delete/{vehicle_id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteDriver([FromRoute] string vehicle_id)
        {
            try
            {
                var deleteDriver = await _driverRepository.DeleteDriver(Guid.Parse(vehicle_id));

                if(deleteDriver.StatusCode == 500)
                {
                    return BadRequest(deleteDriver);
                }

                if(deleteDriver.StatusCode == 404)
                {
                    return NotFound(deleteDriver);
                }

                return Ok(deleteDriver);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("Update/{vehicle_id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequestDTO updateDriver, [FromRoute] string vehicle_id)
        {
            try
            {
                var updatingDriver = await _driverRepository.UpdateDriver(updateDriver, Guid.Parse(vehicle_id));

                if(updatingDriver.StatusCode == 500)
                {
                    return BadRequest(updatingDriver);
                }

                if(updatingDriver.StatusCode == 404)
                {
                    return NotFound(updatingDriver);
                }

                return Ok(updatingDriver);
            }catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("User/{user_id}")]
        public async Task<IActionResult> FetchDriverIdToUserId([FromRoute] string user_id)
        {
            try
            {
                var driverResponse = await _driverRepository.FetchDriverIdToUserId(Guid.Parse(user_id));

                if(driverResponse.StatusCode == 500)
                {
                    return BadRequest(driverResponse);
                }

                if(driverResponse.StatusCode == 404)
                {
                    return NotFound(driverResponse);
                }

                return Ok(driverResponse);
            }catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
