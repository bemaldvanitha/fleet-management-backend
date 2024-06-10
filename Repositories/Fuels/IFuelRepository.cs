using fleet_management_backend.Models.DTO.Fuel;

namespace fleet_management_backend.Repositories.Fuels
{
    public interface IFuelRepository
    {
        public Task<FuelResponseDTO> RecordVehicleFuelLevel(FuelLevelRecordRequestDTO FuelLevelRecordRequest);

        public Task<AllFuelLevelRecordResponseDTO> GetFuelLevelRecordsByVehicle(Guid Id);

        public Task<FuelResponseDTO> FuelRefill(FuelRefillRequestDTO fuelRefillRequest);

        public Task<AllFuelRefillResponseDTO> GetFuelRefillByVehicle(Guid Id);

        public Task<AllFuelRefillResponseDTO> GetFuelRefills();
    }
}
