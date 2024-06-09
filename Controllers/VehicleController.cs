using fleet_management_backend.Models.DTO.Vehicle;
using fleet_management_backend.Repositories.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            this._vehicleRepository = vehicleRepository;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddVehicle([FromBody] AddVehicleRequestDTO addVehicleRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                AddVehicleResponseDTO addVehicleResponse = await _vehicleRepository.AddVehicle(addVehicleRequest);

                if(addVehicleResponse.StatusCode == 500)
                {
                    return BadRequest(addVehicleResponse);
                }

                return Ok(addVehicleResponse);

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetALlVehicles()
        {
            try
            {
                GetAllVehicleResponseDTO getAllVehicleResponse = await _vehicleRepository.GetAllVehicles();

                if(getAllVehicleResponse.StatusCode == 500)
                {
                    return BadRequest(getAllVehicleResponse);
                }
                
                return Ok(getAllVehicleResponse);

            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("Available")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetAvailableVehicles()
        {
            try
            {
                var vehicleResponse = await _vehicleRepository.GetAvailableVehicles();

                if(vehicleResponse.StatusCode == 500)
                {
                    return BadRequest(vehicleResponse);
                }

                return Ok(vehicleResponse);
            }catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("{vehicle_id}")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetSingleVehicle([FromRoute] string vehicle_id)
        {
            try
            {
                GetSingleVehicleResponseDTO getSingleVehicleResponse = await _vehicleRepository.GetSingleVehicle(Guid.Parse(vehicle_id));

                if(getSingleVehicleResponse.StatusCode == 500)
                {
                    return BadRequest(getSingleVehicleResponse);
                }

                if(getSingleVehicleResponse.StatusCode == 400)
                {
                    return NotFound(getSingleVehicleResponse);
                }

                return Ok(getSingleVehicleResponse);

            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpDelete]
        [Route("Delete/{vehicle_id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> RemoveVehicle([FromRoute] string vehicle_id)
        {
            try
            {
                VehicleResponseDTO vehicleResponse = await _vehicleRepository.RemoveVehicle(Guid.Parse(vehicle_id));

                if (vehicleResponse.StatusCode == 500)
                {
                    return BadRequest(vehicleResponse);
                }

                if (vehicleResponse.StatusCode == 400)
                {
                    return NotFound(vehicleResponse);
                }

                return Ok(vehicleResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("Status/{vehicle_id}")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> ChangeStatusOfVehicle([FromRoute] string vehicle_id)
        {
            try
            {
                VehicleResponseDTO vehicleResponse = await _vehicleRepository.ChangeStatus(Guid.Parse(vehicle_id));

                if(vehicleResponse.StatusCode == 500) 
                {
                    return BadRequest(vehicleResponse);
                }

                if(vehicleResponse.StatusCode == 400)
                {
                    return NotFound(vehicleResponse);
                }

                return Ok(vehicleResponse);

            }catch(Exception ex) 
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("{vehicle_id}")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> UpdateVehicleCertificates([FromRoute] string vehicle_id, [FromBody] UpdateVehicleRequestDTO vehicleRequest)
        {
            try
            {
                VehicleResponseDTO vehicleResponse = await _vehicleRepository.UpdateVehicleCertificates(
                    Guid.Parse(vehicle_id), vehicleRequest);

                if(vehicleResponse.StatusCode == 500)
                {
                    return BadRequest(vehicleResponse);
                }

                if(vehicleResponse.StatusCode == 400)
                {
                    return NotFound(vehicleResponse);
                }

                return Ok(vehicleResponse);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
