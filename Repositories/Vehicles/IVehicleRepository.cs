﻿using fleet_management_backend.Models.DTO.Vehicle;

namespace fleet_management_backend.Repositories.Vehicles
{
    public interface IVehicleRepository
    {
        public Task<AddVehicleResponseDTO> AddVehicle(AddVehicleRequestDTO addVehicleRequest);

        public Task<GetAllVehicleResponseDTO> GetAllVehicles();

        public Task<GetSingleVehicleResponseDTO> GetSingleVehicle(Guid id);
    }
}
