using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Repositories.Vehicles
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly FleetManagerDbContext _context;

        public VehicleRepository(FleetManagerDbContext context)
        {
            this._context = context;
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
    }
}
