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
    }
}
