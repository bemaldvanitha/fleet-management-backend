using fleet_management_backend.Models.DTO.Maintenance;
using fleet_management_backend.Repositories.Maintenances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public MaintenanceController(IMaintenanceRepository maintenanceRepository)
        {
            this._maintenanceRepository = maintenanceRepository;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> AddMaintenanceToVehicle(AddMaintenanceRequestDTO addMaintenanceRequest)
        {
            try
            {
                var addedMaintenance = await _maintenanceRepository.CreateVehicleMaintenance(addMaintenanceRequest);

                if(addedMaintenance.StatusCode == 500)
                {
                    return BadRequest(addedMaintenance);
                }

                return Ok(addedMaintenance);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("{vehicle_id}")]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetAllMaintenanceByVehicle([FromRoute] string vehicle_id)
        {
            try
            {
                var allMaintenanceByVehicle = await _maintenanceRepository.GetAllMaintenancesByVehicle(Guid.Parse(vehicle_id));

                if(allMaintenanceByVehicle.StatusCode == 500) 
                { 
                    return BadRequest(allMaintenanceByVehicle);
                }

                return Ok(allMaintenanceByVehicle);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrFleetManagerPolicy")]
        public async Task<IActionResult> GetAllMaintenance()
        {
            try
            {
                var allMaintenances = await _maintenanceRepository.GetAllMaintenances();

                if(allMaintenances.StatusCode == 500)
                {
                    return BadRequest(allMaintenances);
                }

                return Ok(allMaintenances);
            }catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
