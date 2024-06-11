using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Maintenances
{
    public class MaintenanceRepository: IMaintenanceRepository
    {
        private readonly FleetManagerDbContext _context;

        public MaintenanceRepository(FleetManagerDbContext context)
        {
            this._context = context;
        }

        public async Task<MaintenanceResponseDTO> CreateVehicleMaintenance(AddMaintenanceRequestDTO addMaintenanceRequest)
        {
            try
            {
                var maintenanceType = new MaintenanceType
                {
                    Type = addMaintenanceRequest.MaintenanceType
                };

                await _context.MaintenanceType.AddAsync(maintenanceType);
                await _context.SaveChangesAsync();

                var totalCost = 0;

                foreach(var part in addMaintenanceRequest.VehicleParts)
                {
                    totalCost += part.Cost;
                }

                var vehicleMaintenance = new Maintenance
                {
                    IsRegular = addMaintenanceRequest.IsRegular,
                    MaintenanceTypeId = maintenanceType.Id,
                    TotalCost = totalCost,
                    VehicleId = addMaintenanceRequest.VehicleId,
                };

                await _context.Maintenance.AddAsync(vehicleMaintenance);
                await _context.SaveChangesAsync();

                foreach(var part in addMaintenanceRequest.VehicleParts)
                {
                    var vehiclePart = await _context.VehiclePart.FirstOrDefaultAsync(x => x.VehiclePartName == part.VehiclePartName);

                    if(vehiclePart == null)
                    {
                        vehiclePart = new VehiclePart
                        {
                            VehiclePartName = part.VehiclePartName,
                        };

                        await _context.VehiclePart.AddAsync(vehiclePart);
                        await _context.SaveChangesAsync();
                    }

                    var vehiclePartMaintenance = new VehiclePartMaintenance
                    {
                        Cost = part.Cost,
                        MaintenanceId = vehicleMaintenance.Id,
                        VehiclePartId = vehiclePart.Id,
                    };

                    await _context.VehiclePartMaintenances.AddAsync(vehiclePartMaintenance);
                    await _context.SaveChangesAsync();
                }

                return new MaintenanceResponseDTO
                {
                    Message = "Vehicle Maintenance Record Added",
                    StatusCode = 200
                };
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new MaintenanceResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<AllMaintenanceResponseDTO> GetAllMaintenances()
        {
            try
            {
                var allMaintenancesToFleet = await _context.Maintenance.Include(x => x.MaintenanceType)
                    .Include(x => x.VehiclePartsInMaintenance).ThenInclude(x => x.VehiclePart).ToListAsync();

                var allMaintenances = new List<MaintenanceObject>();

                foreach (var maintenance in allMaintenancesToFleet)
                {
                    var maintenanceParts = new List<MaintenancePartObject>();

                    foreach (var maintenancePart in maintenance.VehiclePartsInMaintenance)
                    {
                        var part = new MaintenancePartObject
                        {
                            Cost = maintenancePart.Cost,
                            PartName = maintenancePart.VehiclePart.VehiclePartName
                        };

                        maintenanceParts.Add(part);
                    }

                    var maintenanceObj = new MaintenanceObject
                    {
                        Id = maintenance.Id,
                        IsRegular = maintenance.IsRegular,
                        VehicleId = maintenance.VehicleId,
                        MaintenanceParts = maintenanceParts,
                        MaintenanceType = maintenance.MaintenanceType.Type,
                        TotalCost = maintenance.TotalCost
                    };

                    allMaintenances.Add(maintenanceObj);
                }

                return new AllMaintenanceResponseDTO
                {
                    Maintenances = allMaintenances,
                    Message = "Fetch All Maintenances",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new AllMaintenanceResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<AllMaintenanceResponseDTO> GetAllMaintenancesByVehicle(Guid Id)
        {
            try
            {
                var allMaintenancesByVehicle = await _context.Maintenance.Include(x => x.MaintenanceType)
                    .Include(x => x.VehiclePartsInMaintenance).ThenInclude(x => x.VehiclePart).Where(x => x.VehicleId == Id)
                    .Take(5).ToListAsync();

                var allMaintenances = new List<MaintenanceObject>();

                foreach (var maintenance in allMaintenancesByVehicle)
                {
                    var maintenanceParts = new List<MaintenancePartObject>();

                    foreach(var maintenancePart in maintenance.VehiclePartsInMaintenance)
                    {
                        var part = new MaintenancePartObject
                        {
                            Cost = maintenancePart.Cost,
                            PartName = maintenancePart.VehiclePart.VehiclePartName
                        };

                        maintenanceParts.Add(part);
                    }

                    var maintenanceObj = new MaintenanceObject
                    {
                        Id = maintenance.Id,
                        IsRegular = maintenance.IsRegular,
                        VehicleId = maintenance.VehicleId,
                        MaintenanceParts = maintenanceParts,
                        MaintenanceType = maintenance.MaintenanceType.Type,
                        TotalCost = maintenance.TotalCost
                    };

                    allMaintenances.Add(maintenanceObj);
                }

                return new AllMaintenanceResponseDTO
                {
                    Maintenances = allMaintenances,
                    Message = "Fetch All Maintenance By Vehicle",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new AllMaintenanceResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
                
        }
    }
}
