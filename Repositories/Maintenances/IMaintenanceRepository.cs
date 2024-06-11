using fleet_management_backend.Models.DTO.Maintenance;

namespace fleet_management_backend.Repositories.Maintenances
{
    public interface IMaintenanceRepository
    {
        public Task<MaintenanceResponseDTO> CreateVehicleMaintenance(AddMaintenanceRequestDTO addMaintenanceRequestDTO);

        public Task<AllMaintenanceResponseDTO> GetAllMaintenancesByVehicle(Guid Id);

        public Task<AllMaintenanceResponseDTO> GetAllMaintenances();
    }
}
