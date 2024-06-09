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

                if(tripCreate.StatusCode == 500)
                {
                    return BadRequest(tripCreate);
                }

                return Ok(tripCreate);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
