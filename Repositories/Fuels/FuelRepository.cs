using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Fuel;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Fuels
{
    public class FuelRepository : IFuelRepository
    {
        private readonly FleetManagerDbContext _context;

        public FuelRepository(FleetManagerDbContext context)
        {
            this._context = context;
        }

        public async Task<FuelResponseDTO> FuelRefill(FuelRefillRequestDTO fuelRefillRequest)
        {
            try
            {
                var fuelRefillLocation = new Location
                {
                    Address = fuelRefillRequest.FuelRefillLocation.Address,
                    Latitude = fuelRefillRequest.FuelRefillLocation.Latitude,
                    Longitude = fuelRefillRequest.FuelRefillLocation.Longitude
                };

                await _context.Location.AddAsync(fuelRefillLocation);
                await _context.SaveChangesAsync();

                var beforeRefillLevel = new VehicleFuelLevel
                {
                    CurrentLevel = fuelRefillRequest.StartFuelLevel,
                    VehicleId = fuelRefillRequest.VehicleId,
                    LocationId = fuelRefillLocation.Id,
                };

                var afterRefillLevel = new VehicleFuelLevel
                {
                    CurrentLevel = fuelRefillRequest.EndFuelLevel,
                    LocationId = fuelRefillLocation.Id,
                    VehicleId = fuelRefillRequest.VehicleId
                };

                await _context.VehicleFuelLevels.AddAsync(beforeRefillLevel);
                await _context.VehicleFuelLevels.AddAsync(afterRefillLevel);
                await _context.SaveChangesAsync();

                var vehicleFuel = new Fuel
                {
                    Cost = fuelRefillRequest.Cost,
                    FuelAmount = fuelRefillRequest.FuelAmount,
                    LocationId = fuelRefillLocation.Id,
                    VehicleId = fuelRefillRequest.VehicleId,
                };

                await _context.Fuel.AddAsync(vehicleFuel);
                await _context.SaveChangesAsync();

                return new FuelResponseDTO
                {
                    Message = "Fuel Refill Record Added",
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new FuelResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<AllFuelLevelRecordResponseDTO> GetFuelLevelRecordsByVehicle(Guid Id)
        {
            try
            {
                var allFuelLevelByVehicle = await _context.VehicleFuelLevels.Include(x => x.FuelLocation)
                    .Where(x => x.VehicleId == Id).Take(5).ToListAsync();

                var allFuelRefills = new List<FuelLevelObj>();

                foreach(var fuel in allFuelLevelByVehicle)
                {
                    var fuelLocation = new FuelLevelLocation
                    {
                        Address = fuel.FuelLocation?.Address ?? "",
                        Latitude = fuel.FuelLocation?.Latitude ?? 0,
                        Longitude = fuel.FuelLocation?.Longitude ?? 0,
                    };

                    var fuelObj = new FuelLevelObj
                    {
                        CurrentLevel = fuel.CurrentLevel,
                        Location = fuelLocation
                    };

                    allFuelRefills.Add(fuelObj);
                }

                return new AllFuelLevelRecordResponseDTO
                {
                    FuelLevel = allFuelRefills,
                    Message = "Latest 5 Fuel Levels Recorded",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new AllFuelLevelRecordResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<AllFuelRefillResponseDTO> GetFuelRefillByVehicle(Guid Id)
        {
            try
            {
                var allRefillsToVehicle = await _context.Fuel.Include(x => x.Location).Take(10).Where(x => x.VehicleId == Id)
                    .ToListAsync();

                var allRefillList = new List<FuelRefillObject>();

                foreach(var refill in allRefillsToVehicle)
                {
                    var refillObj = new FuelRefillObject
                    {
                        Cost = refill.Cost,
                        FuelAmount = refill.FuelAmount,
                        Location = new FuelRefillLocationObject
                        {
                            Address = refill.Location?.Address ?? "",
                            Latitude = refill.Location?.Latitude ?? 0,
                            Longitude = refill.Location?.Longitude ?? 0,
                        }
                    };

                    allRefillList.Add(refillObj);
                }

                return new AllFuelRefillResponseDTO
                {
                    FuelRefills = allRefillList,
                    Message = "Fetched Latest Fuel Refills To Vehicle",
                    StatusCode = 200
                };
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new AllFuelRefillResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<AllFuelRefillResponseDTO> GetFuelRefills()
        {
            try
            {
                var allRefillsToVehicle = await _context.Fuel.Include(x => x.Location).ToListAsync();

                var allRefillList = new List<FuelRefillObject>();

                foreach (var refill in allRefillsToVehicle)
                {
                    var refillObj = new FuelRefillObject
                    {
                        Cost = refill.Cost,
                        FuelAmount = refill.FuelAmount,
                        Location = new FuelRefillLocationObject
                        {
                            Address = refill.Location?.Address ?? "",
                            Latitude = refill.Location?.Latitude ?? 0,
                            Longitude = refill.Location?.Longitude ?? 0,
                        }
                    };

                    allRefillList.Add(refillObj);
                }

                return new AllFuelRefillResponseDTO
                {
                    FuelRefills = allRefillList,
                    Message = "Fetched All Fuel Refills",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new AllFuelRefillResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<FuelResponseDTO> RecordVehicleFuelLevel(FuelLevelRecordRequestDTO fuelLevelRecordRequest)
        {
            try
            {
                var fuelLocation = new Location
                {
                    Latitude = fuelLevelRecordRequest.Location.Latitude,
                    Longitude = fuelLevelRecordRequest.Location.Longitude,
                    Address = fuelLevelRecordRequest.Location.Address
                };

                await _context.Location.AddAsync(fuelLocation);
                await _context.SaveChangesAsync();

                var vehicleFuelLevel = new VehicleFuelLevel
                {
                    CurrentLevel = fuelLevelRecordRequest.CurrentLevel,
                    LocationId = fuelLocation.Id,
                    VehicleId = fuelLevelRecordRequest.VehicleId
                };

                await _context.VehicleFuelLevels.AddAsync(vehicleFuelLevel);
                await _context.SaveChangesAsync();

                return new FuelResponseDTO
                {
                    Message = "Fuel level recorded",
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new FuelResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
