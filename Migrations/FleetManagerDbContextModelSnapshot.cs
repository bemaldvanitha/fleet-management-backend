﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fleet_management_backend.Data;

#nullable disable

namespace fleet_management_backend.Migrations
{
    [DbContext(typeof(FleetManagerDbContext))]
    partial class FleetManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DriverStatusId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LicenceNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DriverStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.DriverCertification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Certification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("DriverCertificationTypeId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DriverCertificationTypeId");

                    b.HasIndex("DriverId");

                    b.ToTable("DriverCertifications");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.DriverCertificationType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DriverCertificationsType");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.DriverStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DriverStatus");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.DriverVehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("VehicleId");

                    b.ToTable("DriverVehicles");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Fuel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int>("FuelAmount")
                        .HasColumnType("int");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Fuel");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Maintenance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsRegular")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("MaintenanceTypeId")
                        .HasColumnType("char(36)");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceTypeId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Maintenance");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.MaintenanceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MaintenanceType");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EndLocationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StartLocationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("EndLocationId");

                    b.HasIndex("StartLocationId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.TripLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TripId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("TripId");

                    b.ToTable("TripLocation");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.TripStop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TripId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("TripId");

                    b.ToTable("TripStop");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserTypeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("MobileNumber")
                        .IsUnique();

                    b.HasIndex("UserTypeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.UserType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ManufacturedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("PurchasedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("VehicleBrandId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VehicleModelId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VehicleStatusId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("VehicleBrandId");

                    b.HasIndex("VehicleModelId");

                    b.HasIndex("VehicleStatusId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleBrand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("VehicleBrands");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleCertification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Certification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("VehicleCertificationTypeId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("VehicleCertificationTypeId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleCertifications");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleCertificationType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("VehicleCertificationTypes");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleFuelLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CurrentLevel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleFuelLevels");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("VehicleModel");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehiclePart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("VehiclePartName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("VehiclePart");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehiclePartMaintenance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<Guid>("MaintenanceId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("VehiclePartId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceId");

                    b.HasIndex("VehiclePartId");

                    b.ToTable("VehiclePartMaintenances");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("VehicleStatus");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Driver", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.DriverStatus", "DriverStatus")
                        .WithMany()
                        .HasForeignKey("DriverStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DriverStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.DriverCertification", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.DriverCertificationType", "CertificationType")
                        .WithMany()
                        .HasForeignKey("DriverCertificationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Driver", null)
                        .WithMany("DriverCertifications")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CertificationType");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.DriverVehicle", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Fuel", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.Vehicle", null)
                        .WithMany("VehicleFuelRefills")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Maintenance", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.MaintenanceType", "MaintenanceType")
                        .WithMany()
                        .HasForeignKey("MaintenanceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Vehicle", null)
                        .WithMany("Maintenances")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaintenanceType");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Trip", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Location", "EndLocation")
                        .WithMany()
                        .HasForeignKey("EndLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Location", "StartLocation")
                        .WithMany()
                        .HasForeignKey("StartLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("EndLocation");

                    b.Navigation("StartLocation");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.TripLocation", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Trip", null)
                        .WithMany("TripLocations")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.TripStop", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.Location", "StopLocation")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Trip", null)
                        .WithMany("TripStops")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StopLocation");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.User", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Vehicle", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.VehicleBrand", "VehicleBrand")
                        .WithMany()
                        .HasForeignKey("VehicleBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.VehicleModel", "VehicleModel")
                        .WithMany()
                        .HasForeignKey("VehicleModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.VehicleStatus", "VehicleStatus")
                        .WithMany()
                        .HasForeignKey("VehicleStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleBrand");

                    b.Navigation("VehicleModel");

                    b.Navigation("VehicleStatus");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleCertification", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.VehicleCertificationType", "CertificationType")
                        .WithMany()
                        .HasForeignKey("VehicleCertificationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Vehicle", null)
                        .WithMany("VehicleCertifications")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CertificationType");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehicleFuelLevel", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.Location", "FuelLocation")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.Vehicle", null)
                        .WithMany("VehicleFuelLevels")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelLocation");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.VehiclePartMaintenance", b =>
                {
                    b.HasOne("fleet_management_backend.Models.Domains.Maintenance", null)
                        .WithMany("VehiclePartsInMaintenance")
                        .HasForeignKey("MaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fleet_management_backend.Models.Domains.VehiclePart", "VehiclePart")
                        .WithMany()
                        .HasForeignKey("VehiclePartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehiclePart");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Driver", b =>
                {
                    b.Navigation("DriverCertifications");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Maintenance", b =>
                {
                    b.Navigation("VehiclePartsInMaintenance");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Trip", b =>
                {
                    b.Navigation("TripLocations");

                    b.Navigation("TripStops");
                });

            modelBuilder.Entity("fleet_management_backend.Models.Domains.Vehicle", b =>
                {
                    b.Navigation("Maintenances");

                    b.Navigation("VehicleCertifications");

                    b.Navigation("VehicleFuelLevels");

                    b.Navigation("VehicleFuelRefills");
                });
#pragma warning restore 612, 618
        }
    }
}
