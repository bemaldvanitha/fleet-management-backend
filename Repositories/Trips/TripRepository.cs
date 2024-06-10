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
                    Address = tripLocationRequest.Address
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
                    Longitude = startTripRequest.StartLocation?.Longitude ?? 0,
                    Address = startTripRequest.StartLocation?.Address ?? "",
                };

                var endLocation = new Location
                {
                    Latitude = startTripRequest.EndLocation?.Latitude ?? 0,
                    Longitude = startTripRequest.EndLocation?.Longitude ?? 0,
                    Address = startTripRequest.EndLocation?.Address ?? ""
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

        public async Task<TripListResponseDTO> FetchAllTrips()
        {
            try
            {
                var allTrips = await _context.Trip.Include(x => x.StartLocation)
                    .Include(x => x.EndLocation).Include(x => x.Vehicle).Include(x => x.Driver).ToListAsync();

                var tripList = new List<TripListObject>();

                foreach (var trip in allTrips)
                {
                    var tripStatus = "";

                    if(trip.StartTime == null)
                    {
                        tripStatus = "Pending";
                    }
                    else if(trip.EndTime == null)
                    {
                        tripStatus = "Driving";
                    }
                    else
                    {
                        tripStatus = "Ended";
                    }

                    var tripObject = new TripListObject
                    {
                        Id = trip.Id,
                        DriverName = trip.Driver.FirstName + " " + trip.Driver.LastName,
                        VehicleVIN = trip.Vehicle.VIN,
                        EndLocation = trip.EndLocation?.Address ?? "",
                        StartLocation = trip.StartLocation?.Address ?? "",
                        TripStatus = tripStatus
                    };

                    tripList.Add(tripObject);
                }
                return new TripListResponseDTO
                {
                    Trips = tripList,
                    Message = "All Trips Fetched",
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new TripListResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<TripListResponseDTO> FetchTripsByDriver(Guid Id)
        {
            try
            {
                var allTrips = await _context.Trip.Include(x => x.StartLocation).Include(x => x.EndLocation).Include(x => x.Vehicle)
                    .Include(x => x.Driver).Where(x => x.DriverId == Id).ToListAsync();

                var tripList = new List<TripListObject>();

                foreach (var trip in allTrips)
                {
                    var tripStatus = "";

                    if (trip.StartTime == null)
                    {
                        tripStatus = "Pending";
                    }
                    else if (trip.EndTime == null)
                    {
                        tripStatus = "Driving";
                    }
                    else
                    {
                        tripStatus = "Ended";
                    }

                    var tripObject = new TripListObject
                    {
                        Id = trip.Id,
                        DriverName = trip.Driver.FirstName + " " + trip.Driver.LastName,
                        VehicleVIN = trip.Vehicle.VIN,
                        EndLocation = trip.EndLocation?.Address ?? "",
                        StartLocation = trip.StartLocation?.Address ?? "",
                        TripStatus = tripStatus
                    };

                    tripList.Add(tripObject);
                }

                return new TripListResponseDTO
                {
                    Trips = tripList,
                    Message = "All Trip Assign To Driver Fetched",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new TripListResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<TripListResponseDTO> FetchTripsByVehicle(Guid Id)
        {
            try
            {
                var allTrips = await _context.Trip.Include(x => x.StartLocation).Include(x => x.EndLocation).Include(x => x.Vehicle)
                    .Include(x => x.Driver).Where(x => x.VehicleId == Id).ToListAsync();

                var tripList = new List<TripListObject>();

                foreach (var trip in allTrips)
                {
                    var tripStatus = "";

                    if (trip.StartTime == null)
                    {
                        tripStatus = "Pending";
                    }
                    else if (trip.EndTime == null)
                    {
                        tripStatus = "Driving";
                    }
                    else
                    {
                        tripStatus = "Ended";
                    }

                    var tripObject = new TripListObject
                    {
                        Id = trip.Id,
                        DriverName = trip.Driver.FirstName + " " + trip.Driver.LastName,
                        VehicleVIN = trip.Vehicle.VIN,
                        EndLocation = trip.EndLocation?.Address ?? "",
                        StartLocation = trip.StartLocation?.Address ?? "",
                        TripStatus = tripStatus
                    };

                    tripList.Add(tripObject);
                }

                return new TripListResponseDTO
                {
                    Trips = tripList,
                    Message = "All Trip Assign To Vehicle Fetched",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new TripListResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<SingleTripResponseDTO> SingleTripResponseDTO(Guid Id)
        {
            try
            {
                var trip = await _context.Trip.Include(x => x.TripCertifications).ThenInclude(x => x.TripCertificationType)
                    .Include(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                    .Include(x => x.Driver).Include(x => x.TripLocations).ThenInclude(x => x.Location).Include(x => x.TripStops)
                    .ThenInclude(x => x.StopLocation).FirstOrDefaultAsync(x => x.Id == Id);

                if (trip == null)
                {
                    return new SingleTripResponseDTO
                    {
                        Message = "Trip Not Found",
                        StatusCode = 404
                    };
                }

                var tripCertificationList = new List<TripCertificationObj>();

                foreach(var tripCertification in trip.TripCertifications)
                {
                    var certificate = new TripCertificationObj
                    {
                        Certificate = tripCertification.Certification,
                        CertificateType = tripCertification.TripCertificationType.Type
                    };

                    tripCertificationList.Add(certificate);
                }

                var tripLocationList = new List<TripLocationObj>();

                foreach(var tripLocationItem in trip.TripLocations)
                {
                    var location = new TripLocationObj
                    {
                        Address = tripLocationItem.Location.Address,
                        Latitude = tripLocationItem.Location.Latitude,
                        Longitude = tripLocationItem.Location.Longitude,
                    };

                    tripLocationList.Add(location);
                }

                var tripStopList = new List<TripStopObject>();

                foreach(var stopItem in trip.TripStops)
                {
                    var stopLocation = new TripLocationObj
                    {
                        Latitude = stopItem.StopLocation.Latitude,
                        Longitude = stopItem.StopLocation.Longitude,
                        Address = stopItem.StopLocation.Address
                    };

                    var stop = new TripStopObject
                    {
                        Id = stopItem.Id,
                        EndTime = stopItem?.EndTime ?? DateTime.Now,
                        StartTime = stopItem?.StartTime ?? DateTime.Now,
                        Reason = stopItem?.Reason ?? "",
                        Location = stopLocation
                    };

                    tripStopList.Add(stop);
                }

                var tripFinalObject = new SingleTripObject
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverLicenceNumber = trip.Driver.LicenceNumber,
                    DriverName = trip.Driver.FirstName + " " + trip.Driver.LastName,
                    StartTime = trip?.StartTime ?? DateTime.Now,
                    EndTime = trip?.EndTime ?? DateTime.Now,
                    TripCertifications = tripCertificationList,
                    TripLocations = tripLocationList,
                    TripStops = tripStopList,
                    VehicleDetail = trip.Vehicle.VehicleBrand.Brand + " / " + trip.Vehicle.VehicleModel.Model,
                    VehicleId = trip.VehicleId,
                    VehicleVIN = trip.Vehicle.VIN
                };

                return new SingleTripResponseDTO
                {
                    Message = "Single Trip Fetch",
                    Trip = tripFinalObject,
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new SingleTripResponseDTO 
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

        public async Task<TripResponseDTO> VehicleStopEnd(Guid Id)
        {
            try
            {
                var checkStartedStop = await _context.TripStop.FirstOrDefaultAsync(x => x.TripId == Id && x.EndTime == null);

                if(checkStartedStop == null)
                {
                    return new TripResponseDTO
                    {
                        Message = "Trip Stop Not Found",
                        StatusCode = 404
                    };
                }

                checkStartedStop.EndTime = DateTime.Now;
                await _context.SaveChangesAsync();

                return new TripResponseDTO
                {
                    Message = "Trip Stop Ended",
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

        public async Task<TripResponseDTO> VehicleStopStart(Guid Id, VehicleStopStartRequestDTO vehicleStopStartRequest)
        {
            try
            {
                var location = new Location
                {
                    Latitude = vehicleStopStartRequest.StopLocation.Latitude,
                    Longitude = vehicleStopStartRequest.StopLocation.Longitude,
                    Address = vehicleStopStartRequest.StopLocation.Address
                };

                await _context.Location.AddAsync(location);
                await _context.SaveChangesAsync();

                var vehicleStop = new TripStop
                {
                    LocationId = location.Id,
                    Reason = vehicleStopStartRequest.Reason,
                    StartTime = DateTime.Now,
                    TripId = Id,
                };

                await _context.TripStop.AddAsync(vehicleStop);
                await _context.SaveChangesAsync();

                return new TripResponseDTO
                {
                    Message = "Trip Stop Added",
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

        public async Task<TripResponseDTO> VehicleTripEnd(Guid Id)
        {
            try
            {
                var stopingTrip = await _context.Trip.FirstOrDefaultAsync(t => t.Id == Id);

                if(stopingTrip == null)
                {
                    return new TripResponseDTO
                    {
                        Message = "Trip Not Found",
                        StatusCode = 404
                    };
                }

                stopingTrip.EndTime = DateTime.Now;
                await _context.SaveChangesAsync();

                return new TripResponseDTO
                {
                    StatusCode = 200,
                    Message = "Vehicle Trip Ended"
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
