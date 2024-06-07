using fleet_management_backend.Models.DTO.Vehicle;

namespace fleet_management_backend.Repositories.Vehicle
{
    public interface IVehicleRepository
    {
        public Task<AddVehicleResponseDTO> AddVehicle(AddVehicleRequestDTO addVehicleRequest);
    }
}
