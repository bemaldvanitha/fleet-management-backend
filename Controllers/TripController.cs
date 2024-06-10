using fleet_management_backend.Models.DTO.Trip;
using fleet_management_backend.Repositories.Trips;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            this._tripRepository = tripRepository;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> CreateTrip([FromBody] StartTripRequestDTO startTripRequest)
        {
            try
            {
                var tripCreate = await _tripRepository.CreateTrip(startTripRequest);

                if (tripCreate.StatusCode == 500)
                {
                    return BadRequest(tripCreate);
                }

                return Ok(tripCreate);
            } catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("Start/{vehicle_id}")]
        [Authorize(Policy = "DriverPolicy")]
        public async Task<IActionResult> StartTrip([FromRoute] string vehicle_id)
        {
            try
            {
                var startingTrip = await _tripRepository.StartTrip(Guid.Parse(vehicle_id));

                if (startingTrip.StatusCode == 500)
                {
                    return BadRequest(startingTrip);
                }

                if (startingTrip.StatusCode == 404)
                {
                    return NotFound(startingTrip);
                }

                return Ok(startingTrip);
            } catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("Location/{vehicle_id}")]
        [Authorize(Policy = "DriverPolicy")]
        public async Task<IActionResult> SetCurrentTripLocation([FromRoute] string vehicle_id, [FromBody] TripLocationRequestDTO tripLocationRequest)
        {
            try
            {
                var currentLocation = await _tripRepository.AddCurrentLocation(Guid.Parse(vehicle_id), tripLocationRequest);

                if (currentLocation.StatusCode == 500)
                {
                    return BadRequest(currentLocation);
                }

                return Ok(currentLocation);

            } catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("Stop/Start/{vehicle_id}")]
        [Authorize(Policy = "DriverPolicy")]
        public async Task<IActionResult> VehicleStopStart([FromRoute] string vehicle_id, [FromBody] VehicleStopStartRequestDTO vehicleStopStartRequest)
        {
            try
            {
                var vehicleStop = await _tripRepository.VehicleStopStart(Guid.Parse(vehicle_id), vehicleStopStartRequest);

                if(vehicleStop.StatusCode == 500)
                {
                    return BadRequest(vehicleStop);
                }

                return Ok(vehicleStop);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("Stop/End/{vehicle_id}")]
        [Authorize(Policy = "DriverPolicy")]
        public async Task<IActionResult> VehicleStopEnd([FromRoute] string vehicle_id)
        {
            try
            {
                var vehicalStopEnd = await _tripRepository.VehicleStopEnd(Guid.Parse(vehicle_id));

                if(vehicalStopEnd.StatusCode == 500)
                {
                    return BadRequest(vehicalStopEnd);
                }

                if(vehicalStopEnd.StatusCode == 404)
                {
                    return NotFound(vehicalStopEnd);
                }

                return Ok(vehicalStopEnd);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPatch]
        [Route("End/{vehicle_id}")]
        [Authorize(Policy = "DriverPolicy")]
        public async Task<IActionResult> TripEnded([FromRoute] string vehicle_id)
        {
            try
            {
                var tripStoped = await _tripRepository.VehicleTripEnd(Guid.Parse(vehicle_id));

                if(tripStoped.StatusCode == 500)
                { 
                    return BadRequest(tripStoped); 
                }

                if (tripStoped.StatusCode == 404)
                {
                    return NotFound(tripStoped);
                }

                return Ok(tripStoped);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> FetchAllTrips()
        {
            try
            {
                var tripListResponse = await _tripRepository.FetchAllTrips();

                if(tripListResponse.StatusCode == 500)
                {
                    return BadRequest(tripListResponse);
                }

                return Ok(tripListResponse);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("Driver/{vehicle_id}")]
        [Authorize]
        public async Task<IActionResult> FetchTripsAssignToDriver([FromRoute] string vehicle_id)
        {
            try
            {
                var tripListResponse = await _tripRepository.FetchTripsByDriver(Guid.Parse(vehicle_id));

                if (tripListResponse.StatusCode == 500)
                {
                    return BadRequest(tripListResponse);
                }

                return Ok(tripListResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }


    }
}
