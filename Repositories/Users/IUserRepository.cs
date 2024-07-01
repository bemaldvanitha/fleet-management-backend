using fleet_management_backend.Models.DTO.User;

namespace fleet_management_backend.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<AllDriversResponseDTO> AllDrivers();

        public Task<AllFleetManagerReponseDTO> AllFleetManagers();
    }
}
