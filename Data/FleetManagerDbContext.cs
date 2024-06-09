using fleet_management_backend.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace fleet_management_backend.Data
{
    public class FleetManagerDbContext : DbContext
    {
        public FleetManagerDbContext(DbContextOptions<FleetManagerDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<DriverCertification> DriverCertifications { get; set; }

        public DbSet<DriverCertificationType> DriverCertificationsType { get; set;}

        public DbSet<DriverStatus> DriverStatus { get; set; }

        public DbSet<DriverVehicle> DriverVehicles { get; set; }

        public DbSet<Fuel> Fuel { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<Maintenance> Maintenance { get; set; }

        public DbSet<MaintenanceType> MaintenanceType { get; set;}

        public DbSet<Trip> Trip { get; set; }

        public DbSet<TripCertification> TripCertifications { get; set; }

        public DbSet<TripCertificationType> TripCertificationTypes { get; set; }

        public DbSet<TripLocation> TripLocation { get; set; }

        public DbSet<TripStop> TripStop { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserType> UserType { get; set; }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<VehicleBrand> VehicleBrands { get; set; }

        public DbSet<VehicleCertification> VehicleCertifications { get; set;}

        public DbSet<VehicleCertificationType> VehicleCertificationTypes { get; set; }

        public DbSet<VehicleFuelLevel> VehicleFuelLevels { get; set; }

        public DbSet<VehicleModel> VehicleModel { get; set; }

        public DbSet<VehiclePart> VehiclePart { get; set; }

        public DbSet<VehiclePartMaintenance> VehiclePartMaintenances { get; set; }

        public DbSet<VehicleStatus> VehicleStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(100);

            modelBuilder.Entity<User>().Property(x => x.MobileNumber).HasMaxLength(100);

            modelBuilder.Entity<User>().HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>().HasIndex(u => u.MobileNumber)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
