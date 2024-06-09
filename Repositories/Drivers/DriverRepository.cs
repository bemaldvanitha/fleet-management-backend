using Amazon.S3.Model;
using Amazon.S3;
using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Driver;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Drivers
{
    public class DriverRepository : IDriverRepository
    {
        private readonly FleetManagerDbContext _context;
        private readonly IAmazonS3 _amazonS3;

        public DriverRepository(FleetManagerDbContext context, IAmazonS3 amazonS3)
        {
            this._context = context;
            this._amazonS3 = amazonS3;
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

        public async Task<GetAllDriversResponseDTO> GetAllDrivers()
        {
            try
            {
                var allDrivers = await _context.Drivers.Include(x => x.DriverStatus).Include(x => x.User).ToListAsync();

                List<DriverResponseObj> drivers = new List<DriverResponseObj>();

                foreach (var driver in allDrivers)
                {
                    DriverResponseObj driverObj = new DriverResponseObj
                    {
                        Id = driver.Id,
                        FirstName = driver.FirstName,
                        LastName = driver.LastName,
                        LicenceNumber = driver.LicenceNumber,
                        Status = driver.DriverStatus.Status,
                        MobileNumber = driver.User.MobileNumber,
                    };

                    drivers.Add(driverObj);
                }

                return new GetAllDriversResponseDTO 
                {
                    Drivers = drivers,
                    Message = "All Drivers Fetched",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new GetAllDriversResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<GetSingleDriverResponseDTO> GetSingleDriver(Guid Id)
        {
            try
            {
                string bucketName = "myfleetmanager";
                DateTime expiration = DateTime.Now.AddHours(1);

                var singleDriver = await _context.Drivers.Include(x => x.DriverStatus).Include(x => x.DriverCertifications)
                    .ThenInclude(certificate => certificate.CertificationType).Include(u => u.User)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if(singleDriver == null)
                {
                    return new GetSingleDriverResponseDTO
                    {
                        Message = "No Driver Found",
                        StatusCode = 404
                    };
                }

                List<SingleDriverCertificate> driverCertificates = new List<SingleDriverCertificate>();

                foreach(var certificate in singleDriver.DriverCertifications)
                {
                    GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                    {
                        BucketName = bucketName,
                        Key = "driver/" + certificate.Certification,
                        Protocol = Protocol.HTTPS,
                        Expires = expiration,
                        Verb = HttpVerb.GET
                    };

                    string url = _amazonS3.GetPreSignedURL(request);

                    var certificateObj = new SingleDriverCertificate
                    {
                        Certificate = url,
                        CertificationType = certificate.CertificationType.Type,
                    };

                    driverCertificates.Add(certificateObj);
                }

                return new GetSingleDriverResponseDTO
                {
                    Message = "Fetch Driver",
                    StatusCode = 200,
                    Driver = new SingleDriverObj
                    {
                        FirstName = singleDriver.FirstName,
                        LastName = singleDriver.LastName,
                        Id = Id,
                        LicenceNumber = singleDriver.LicenceNumber,
                        Status = singleDriver.DriverStatus.Status,
                        Certificates = driverCertificates
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new GetSingleDriverResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
                
        }
    }
}
