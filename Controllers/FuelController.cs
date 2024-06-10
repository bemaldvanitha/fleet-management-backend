using fleet_management_backend.Models.DTO.Fuel;
using fleet_management_backend.Repositories.Fuels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelController : ControllerBase
    {
        private readonly IFuelRepository _fuelRepository;

        public FuelController(IFuelRepository fuelRepository)
        {
            this._fuelRepository = fuelRepository;
        }

        [HttpPost]
        [Route("Level")]
        [Authorize(Policy = "DriverPolicy")]
        public async Task<IActionResult> RecordFuelLevel(FuelLevelRecordRequestDTO fuelLevelRecordRequest)
        {
            try
            {
                var recordedFuelRecord = await _fuelRepository.RecordVehicleFuelLevel(fuelLevelRecordRequest);

                if(recordedFuelRecord.StatusCode == 500)
                {
                    return BadRequest(recordedFuelRecord);
                }

                return Ok(recordedFuelRecord);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("Level/{vehicle_id}")]
        [Authorize]
        public async Task<IActionResult> FetchLatest5FuelRecordsForVehicle([FromRoute] string vehicle_id)
        {
            try
            {
                var latestRecord = await _fuelRepository.GetFuelLevelRecordsByVehicle(Guid.Parse(vehicle_id));

                if(latestRecord.StatusCode == 500)
                {
                    return BadRequest(latestRecord);
                }

                return Ok(latestRecord);
            }catch (Exception ex) 
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        [Route("Refill")]
        [Authorize(Policy = "DriverPolicy")]
        public async Task<IActionResult> RefillVehicle(FuelRefillRequestDTO fuelRefillRequest)
        {
            try
            {
                var fuelRefill = await _fuelRepository.FuelRefill(fuelRefillRequest);

                if(fuelRefill.StatusCode == 500) 
                {
                    return BadRequest(fuelRefill);
                }

                return Ok(fuelRefill);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("Refill/{vehicle_id}")]
        [Authorize]
        public async Task<IActionResult> FetchLatestVehicleRefill([FromRoute] string vehicle_id)
        {
            try
            {
                var fuelRefills = await _fuelRepository.GetFuelRefillByVehicle(Guid.Parse(vehicle_id));

                if(fuelRefills.StatusCode == 500)
                {
                    return BadRequest(fuelRefills);
                }

                return Ok(fuelRefills);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("Refill")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> FetchAllFuelRefills()
        {
            try
            {
                var fuelRefills = await _fuelRepository.GetFuelRefills();

                if (fuelRefills.StatusCode == 500)
                {
                    return BadRequest(fuelRefills);
                }

                return Ok(fuelRefills);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
