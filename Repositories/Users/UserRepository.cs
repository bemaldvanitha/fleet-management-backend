using fleet_management_backend.Data;
using fleet_management_backend.Models.DTO.User;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Users
{
    public class UserRepository: IUserRepository
    {
        private readonly FleetManagerDbContext _context;

        public UserRepository(FleetManagerDbContext context)
        {
            this._context = context;
        }

        public async Task<AllDriversResponseDTO> AllDrivers()
        {
            try
            {
                var allDrivers = await _context.Drivers.Include(x => x.User).ToListAsync();

                var allDriverList = new List<DriverObject>();

                foreach (var driver in allDrivers)
                {
                    var driverObject = new DriverObject
                    {
                        DriverLicenceNumber = driver.LicenceNumber,
                        Email = driver.User?.Email ?? "",
                        Id = driver.Id,
                        MobileNumber = driver.User?.MobileNumber ?? "",
                        Name = driver.FirstName + " " + driver.LastName,
                        UserId = driver.User?.Id ?? Guid.NewGuid(),
                    };

                    allDriverList.Add(driverObject);
                }

                return new AllDriversResponseDTO
                {
                    Drivers = allDriverList,
                    Message = "All Drivers List",
                    StatusCode = 200
                };
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new AllDriversResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<AllFleetManagerReponseDTO> AllFleetManagers()
        {
            try
            {
                var allFleetManagers = await _context.User.Include(x => x.UserType).Where(x => x.UserType.Type == "Fleet-Manager")
                    .ToListAsync();

                var fleetManagersList = new List<FleetManagerObject>();

                foreach (var fleetManager in allFleetManagers)
                {
                    var fleetManagerObject = new FleetManagerObject
                    {
                        Email = fleetManager?.Email ?? "",
                        Id = fleetManager.Id,
                        Name = fleetManager.DisplayName,
                        MobileNumber = fleetManager.MobileNumber,
                    };

                    fleetManagersList.Add(fleetManagerObject);
                }

                return new AllFleetManagerReponseDTO 
                {
                    FleetManagers = fleetManagersList, 
                    Message = "All Fleet Managers Fetched" ,  
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new AllFleetManagerReponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
