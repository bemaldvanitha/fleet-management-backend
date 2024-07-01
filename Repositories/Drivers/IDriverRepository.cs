using fleet_management_backend.Models.DTO.Driver;

namespace fleet_management_backend.Repositories.Drivers
{
    public interface IDriverRepository
    {
        public Task<DriverResponseDTO> AddDriver(AddDriverRequestDTO addDriverRequest);

        public Task<GetAllDriversResponseDTO> GetAllDrivers();

        public Task<GetAllDriversResponseDTO> GetAvailableDrivers();

        public Task<GetSingleDriverResponseDTO> GetSingleDriver(Guid id);

        public Task<DriverResponseDTO> ChangeDriverStatus(Guid id);

        public Task<DriverResponseDTO> DeleteDriver(Guid id);

        public Task<DriverResponseDTO> UpdateDriver(UpdateDriverRequestDTO updateDriverRequest, Guid Id);

        public Task<FetchDriverIdResponseDTO> FetchDriverIdToUserId(Guid id);
    }
}
