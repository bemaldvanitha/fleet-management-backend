using fleet_management_backend.Models.DTO.Trip;

namespace fleet_management_backend.Repositories.Trips
{
    public interface ITripRepository
    {
        public Task<TripResponseDTO> CreateTrip(StartTripRequestDTO startTripRequest);

        public Task<TripResponseDTO> StartTrip(Guid Id);

        public Task<TripResponseDTO> AddCurrentLocation(Guid Id, TripLocationRequestDTO tripLocationRequest);

        public Task<TripResponseDTO> VehicleStopStart(Guid Id, VehicleStopStartRequestDTO vehicleStopStartRequest);
    }
}
