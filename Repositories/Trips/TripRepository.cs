using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Trip;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Trips
{
    public class TripRepository: ITripRepository
    {
        private readonly FleetManagerDbContext _context;

        public TripRepository(FleetManagerDbContext context)
        {
            this._context = context;
        }

        public async Task<TripResponseDTO> CreateTrip(StartTripRequestDTO startTripRequest)
        {
            try
            {
                var startLocation = new Location
                {
                    Latitude = startTripRequest.StartLocation?.Latitude ?? 0,
                    Longitude = startTripRequest.StartLocation?.Longitude ?? 0
                };

                var endLocation = new Location
                {
                    Latitude = startTripRequest.EndLocation?.Latitude ?? 0,
                    Longitude = startTripRequest.EndLocation?.Longitude ?? 0
                };

                await _context.Location.AddAsync(startLocation);
                await _context.Location.AddAsync(endLocation);
                await _context.SaveChangesAsync();

                var trip = new Trip
                {
                    DriverId = startTripRequest.DriverId,
                    VehicleId = startTripRequest.VehicleId,
                    StartLocationId = startLocation.Id,
                    EndLocationId = endLocation.Id,
                };

                await _context.Trip.AddAsync(trip);
                await _context.SaveChangesAsync();

                foreach(var certification in startTripRequest.Certifications)
                {
                    var tripCertificationType = await _context.TripCertificationTypes.FirstOrDefaultAsync(x => x.Type == 
                        certification.CertificateType);

                    if (tripCertificationType == null)
                    {
                        tripCertificationType = new TripCertificationType
                        {
                            Type = certification.CertificateType,
                        };

                        await _context.TripCertificationTypes.AddAsync(tripCertificationType);
                        await _context.SaveChangesAsync();
                    }

                    var tripCertificate = new TripCertification
                    {
                        TripId = trip.Id,
                        TripCertificationTypeId = tripCertificationType.Id,
                        Certification = certification.Certificate
                    };

                    await _context.TripCertifications.AddAsync(tripCertificate);
                    await _context.SaveChangesAsync();
                }

                return new TripResponseDTO
                {
                    Message = "Trip Created",
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new TripResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
