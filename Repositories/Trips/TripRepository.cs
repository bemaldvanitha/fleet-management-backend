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

        public async Task<TripResponseDTO> AddCurrentLocation(Guid Id, TripLocationRequestDTO tripLocationRequest)
        {
            try
            {
                var location = new Location
                {
                    Latitude = tripLocationRequest.Latitude,
                    Longitude = tripLocationRequest.Longitude,
                };

                await _context.Location.AddAsync(location);
                await _context.SaveChangesAsync();

                var tripLocation = new TripLocation
                {
                    TripId = Id,
                    LocationId = location.Id,
                };

                await _context.TripLocation.AddAsync(tripLocation);
                await _context.SaveChangesAsync();

                return new TripResponseDTO
                {
                    Message = "Current Location Saved",
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new TripResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
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

        public async Task<TripResponseDTO> StartTrip(Guid Id)
        {
            try
            {
                var startingTrip = await _context.Trip.FirstOrDefaultAsync(x => x.Id == Id);

                if(startingTrip == null)
                {
                    return new TripResponseDTO
                    {
                        Message = "Trip Not Found",
                        StatusCode = 404
                    };
                }

                startingTrip.StartTime = DateTime.Now;
                await _context.SaveChangesAsync();

                return new TripResponseDTO
                {
                    Message = "Trip Started",
                    StatusCode = 200
                };

            }catch(Exception ex)
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
