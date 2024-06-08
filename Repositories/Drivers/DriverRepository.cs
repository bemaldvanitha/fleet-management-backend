using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Driver;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Drivers
{
    public class DriverRepository : IDriverRepository
    {
        private readonly FleetManagerDbContext _context;

        public DriverRepository(FleetManagerDbContext context)
        {
            this._context = context;
        }

        public async Task<DriverResponseDTO> AddDriver(AddDriverRequestDTO addDriverRequest)
        {
            try
            {
                var isDriverStatusAvailable = await _context.DriverStatus.FirstOrDefaultAsync(x => x.Status == "Available");

                if (isDriverStatusAvailable == null)
                {
                    isDriverStatusAvailable = new DriverStatus
                    {
                        Status = "Available"
                    };

                    await _context.DriverStatus.AddAsync(isDriverStatusAvailable);
                    await _context.SaveChangesAsync();
                }

                Driver driver = new Driver
                {
                    FirstName = addDriverRequest.FirstName,
                    LastName = addDriverRequest.LastName,
                    DriverStatusId = isDriverStatusAvailable.Id,
                    LicenceNumber = addDriverRequest.LicenceNumber,
                    UserId = Guid.Parse(addDriverRequest.UserId),
                };

                await _context.Drivers.AddAsync(driver);
                await _context.SaveChangesAsync();

                foreach(var certification in addDriverRequest.DriverCertifications)
                {
                    var isDriverCertificationTypeAvailable = await _context.DriverCertificationsType.FirstOrDefaultAsync(x => x.Type == 
                        certification.CertificationType);

                    if(isDriverCertificationTypeAvailable == null)
                    {
                        isDriverCertificationTypeAvailable = new DriverCertificationType
                        {
                            Type = certification.CertificationType,
                        };

                        await _context.DriverCertificationsType.AddAsync(isDriverCertificationTypeAvailable);
                        await _context.SaveChangesAsync();
                    }

                    var driverCertificate = new DriverCertification
                    {
                        Certification = certification.Certificate,
                        DriverCertificationTypeId = isDriverCertificationTypeAvailable.Id,
                        DriverId = driver.Id,
                    };

                    await _context.DriverCertifications.AddAsync(driverCertificate);
                    await _context.SaveChangesAsync();
                }

                return new DriverResponseDTO
                {
                    Message = "Driver Profile Created",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new DriverResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
