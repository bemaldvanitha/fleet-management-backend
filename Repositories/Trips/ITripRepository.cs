using fleet_management_backend.Models.DTO.Trip;

namespace fleet_management_backend.Repositories.Trips
{
    public interface ITripRepository
    {
        public Task<TripResponseDTO> CreateTrip(StartTripRequestDTO startTripRequest);


    }
}
