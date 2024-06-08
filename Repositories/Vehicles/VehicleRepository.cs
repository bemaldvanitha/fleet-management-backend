using Amazon.S3;
using Amazon.S3.Model;
using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Vehicles
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly FleetManagerDbContext _context;
        private readonly IAmazonS3 _amazonS3;

        public VehicleRepository(FleetManagerDbContext context, IAmazonS3 amazonS3)
        {
            this._context = context;
            this._amazonS3 = amazonS3;
        }

        public async Task<AddVehicleResponseDTO> AddVehicle(AddVehicleRequestDTO addVehicleRequest)
        {
            try
            {
                var isBrandAvailable = await _context.VehicleBrands.FirstOrDefaultAsync(x => x.Brand == addVehicleRequest.Brand);

                if (isBrandAvailable == null)
                {
                    isBrandAvailable = new VehicleBrand
                    {
                        Brand = addVehicleRequest.Brand,
                    };

                    await _context.VehicleBrands.AddAsync(isBrandAvailable);
                    await _context.SaveChangesAsync();
                }

                var isModelAvailable = await _context.VehicleModel.FirstOrDefaultAsync(x => x.Model == addVehicleRequest.Model);

                if(isModelAvailable == null)
                {
                    isModelAvailable = new VehicleModel
                    {
                        Model = addVehicleRequest.Model,
                    };

                    await _context.VehicleModel.AddAsync(isModelAvailable);
                    await _context.SaveChangesAsync();
                };

                var isStatusAvailable = await _context.VehicleStatus.FirstOrDefaultAsync(x => x.Status == "Available");

                if(isStatusAvailable == null)
                {
                    isStatusAvailable = new VehicleStatus
                    {
                        Status = "Available"
                    };

                    await _context.VehicleStatus.AddAsync(isStatusAvailable);
                    await _context.SaveChangesAsync();
                };

                var vehicle = new Vehicle
                {
                    VehicleBrandId = isBrandAvailable.Id,
                    VehicleModelId = isModelAvailable.Id,
                    RegistrationNumber = addVehicleRequest.RegistrationNumber,
                    VIN = addVehicleRequest.VIN,
                    ManufacturedAt = addVehicleRequest.ManufacturedAt,
                    PurchasedAt = addVehicleRequest.PurchasedAt,
                    IsActive = addVehicleRequest.IsBrandNew,
                    VehicleStatusId = isStatusAvailable.Id
                };

                await _context.Vehicle.AddAsync(vehicle);
                await _context.SaveChangesAsync();

                foreach(var certification in addVehicleRequest.VehicleCertifications)
                {
                    var isCertificationTypeExist = await _context.VehicleCertificationTypes.FirstOrDefaultAsync(x => x.Type ==
                        certification.CertificationType);

                    if(isCertificationTypeExist == null)
                    {
                        isCertificationTypeExist = new VehicleCertificationType
                        {
                            Type = certification.CertificationType
                        };

                        await _context.VehicleCertificationTypes.AddAsync(isCertificationTypeExist);
                        await _context.SaveChangesAsync();
                    }

                    var vehicleCertificate = new VehicleCertification
                    {
                        Certification = certification.Certificate,
                        VehicleCertificationTypeId = isCertificationTypeExist.Id,
                        VehicleId = vehicle.Id
                    };

                    await _context.VehicleCertifications.AddAsync(vehicleCertificate);
                    await _context.SaveChangesAsync();
                };

                return new AddVehicleResponseDTO
                {
                    Message = "Vehicle Added",
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new AddVehicleResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<VehicleResponseDTO> ChangeStatus(Guid id)
        {
            try
            {
                var changedVehicle = await _context.Vehicle.Include(x => x.VehicleStatus).FirstOrDefaultAsync(x => x.Id == id);

                if (changedVehicle == null)
                {
                    return new VehicleResponseDTO
                    {
                        Message = "Vehcile Not Found",
                        StatusCode = 400
                    };
                }

                if(changedVehicle.VehicleStatus.Status == "Available")
                {
                    var status = await _context.VehicleStatus.FirstOrDefaultAsync(x => x.Status == "Unavailable");

                    if(status == null)
                    {
                        status = new VehicleStatus
                        {
                            Status = "Unavailable"
                        };

                        await _context.VehicleStatus.AddAsync(status);
                        await _context.SaveChangesAsync();
                    }

                    changedVehicle.VehicleStatusId = status.Id;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var status = await _context.VehicleStatus.FirstOrDefaultAsync(x => x.Status == "Available");

                    if (status == null)
                    {
                        status = new VehicleStatus
                        {
                            Status = "Available"
                        };

                        await _context.VehicleStatus.AddAsync(status);
                        await _context.SaveChangesAsync();
                    }

                    changedVehicle.VehicleStatusId = status.Id;
                    await _context.SaveChangesAsync();
                }

                return new VehicleResponseDTO
                {
                    Message = "Status Changed",
                    StatusCode = 200
                };

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new VehicleResponseDTO 
                { 
                    Message = ex.Message,
                    StatusCode = 500 
                };
            }
        }

        public async Task<GetAllVehicleResponseDTO> GetAllVehicles()
        {
            try
            {
                var allVehicles = await _context.Vehicle.Include(x => x.VehicleBrand).Include(x => x.VehicleModel)
                    .Include(x => x.VehicleStatus).ToListAsync();

                List<VehicleResponseObj> vehicleList = new List<VehicleResponseObj>();

                foreach (var vehicle in allVehicles)
                {
                    var vehicleObj = new VehicleResponseObj
                    {
                        Id = vehicle.Id,
                        Brand = vehicle.VehicleBrand.Brand,
                        Model = vehicle.VehicleModel.Model,
                        RegistrationNumber = vehicle.RegistrationNumber,
                        Status = vehicle.VehicleStatus.Status,
                        VIN = vehicle.VIN
                    };

                    vehicleList.Add(vehicleObj);
                }

                return new GetAllVehicleResponseDTO
                {
                    Message = "All Vehicles",
                    StatusCode = 200,
                    Vehicles = vehicleList
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new GetAllVehicleResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<GetSingleVehicleResponseDTO> GetSingleVehicle(Guid id)
        {
            try
            {
                string bucketName = "myfleetmanager";
                DateTime expiration = DateTime.Now.AddHours(1);

                var singleVehicle = await _context.Vehicle.Include(x => x.VehicleModel).Include(x => x.VehicleBrand).Include(x => x.VehicleStatus)
                    .Include(x => x.VehicleCertifications).ThenInclude(certificate => certificate.CertificationType)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if(singleVehicle == null)
                {
                    return new GetSingleVehicleResponseDTO
                    {
                        Message = "Vehicle Not Found",
                        StatusCode = 400
                    };
                }

                List<SingleVehicleCertificate> VehicleCertificates = new List<SingleVehicleCertificate>();

                foreach(var certification in singleVehicle.VehicleCertifications)
                {
                    GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                    {
                        BucketName = bucketName,
                        Key = "vehicle/" + certification.Certification,
                        Protocol = Protocol.HTTPS,
                        Expires = expiration,
                        Verb = HttpVerb.GET
                    };

                    string url = _amazonS3.GetPreSignedURL(request);

                    var certificate = new SingleVehicleCertificate
                    {
                        Certificate = url,
                        CertificateType = certification.CertificationType.Type
                    };

                    VehicleCertificates.Add(certificate);
                }

                return new GetSingleVehicleResponseDTO
                {
                    Message = "Vehicle Data Featched",
                    StatusCode = 200,
                    Vehicle = new SingleVehicleResponse
                    {
                        Id = id,
                        Brand = singleVehicle.VehicleBrand.Brand,
                        IsBrandNew = singleVehicle.IsActive,
                        ManufacturedAt = singleVehicle.ManufacturedAt,
                        RegistrationNumber = singleVehicle.RegistrationNumber,
                        PurchasedAt = singleVehicle.PurchasedAt,
                        Model = singleVehicle.VehicleModel.Model,
                        Status = singleVehicle.VehicleStatus.Status,
                        VIN = singleVehicle.VIN,
                        VehicleCertificates = VehicleCertificates
                    }
                };

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new GetSingleVehicleResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<VehicleResponseDTO> RemoveVehicle(Guid id)
        {
            try
            {
                var deleteVehicle = await _context.Vehicle.FirstOrDefaultAsync(x => x.Id == id);

                if (deleteVehicle == null)
                {
                    return new VehicleResponseDTO
                    {
                        Message = "Vehicle Not Found",
                        StatusCode = 400
                    };
                }

                var deletingVehicleCertifications = await _context.VehicleCertifications.Where(x => x.VehicleId == id).ToListAsync();

                _context.VehicleCertifications.RemoveRange(deletingVehicleCertifications);
                _context.Vehicle.Remove(deleteVehicle);
                await _context.SaveChangesAsync();

                return new VehicleResponseDTO
                {
                    Message = "Vehicle Removed",
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new VehicleResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<VehicleResponseDTO> UpdateVehicleCertificates(Guid id, UpdateVehicleRequestDTO updateVehicleRequest)
        {
            try
            {
                var updatingVehicle = await _context.Vehicle.Include(x => x.VehicleCertifications)
                    .ThenInclude(certification => certification.CertificationType).FirstOrDefaultAsync(x => x.Id == id);

                if (updatingVehicle == null)
                {
                    return new VehicleResponseDTO
                    {
                        Message = "Vehicle Not Found",
                        StatusCode = 400
                    };
                }

                foreach(var certification in updatingVehicle.VehicleCertifications) 
                {
                    var isChanging = updateVehicleRequest.VehicleCertificates.FirstOrDefault(x => x.CertificateType == 
                        certification.CertificationType.Type);

                    if(isChanging != null)
                    {
                        certification.Certification = isChanging.Certificate;
                    }
                }

                await _context.SaveChangesAsync();

                return new VehicleResponseDTO
                {
                    Message = "Data Update Successful",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new VehicleResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
