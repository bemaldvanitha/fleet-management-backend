using fleet_management_backend.Models.DTO.Driver;

namespace fleet_management_backend.Repositories.Drivers
{
    public interface IDriverRepository
    {
        public Task<DriverResponseDTO> AddDriver(AddDriverRequestDTO addDriverRequest);
    }
}
