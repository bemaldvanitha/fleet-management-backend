using fleet_management_backend.Models.DTO.Vehicle;

namespace fleet_management_backend.Repositories.Vehicles
{
    public interface IVehicleRepository
    {
        public Task<AddVehicleResponseDTO> AddVehicle(AddVehicleRequestDTO addVehicleRequest);

        public Task<GetAllVehicleResponseDTO> GetAllVehicles();

        public Task<GetAllVehicleResponseDTO> GetAvailableVehicles();

        public Task<GetSingleVehicleResponseDTO> GetSingleVehicle(Guid id);

        public Task<VehicleResponseDTO> RemoveVehicle(Guid id);

        public Task<VehicleResponseDTO> ChangeStatus(Guid id);

        public Task<VehicleResponseDTO> UpdateVehicleCertificates(Guid id, UpdateVehicleRequestDTO updateVehicleRequest);
    }
}
