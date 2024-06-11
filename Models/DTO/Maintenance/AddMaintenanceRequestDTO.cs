namespace fleet_management_backend.Models.DTO.Maintenance
{
    public class AddMaintenanceRequestDTO
    {
        public Guid VehicleId { get; set; }

        public string MaintenanceType { get; set; }

        public bool IsRegular { get; set; }

        public List<SingleMaintenancePart> VehicleParts { get; set; }
    }

    public class SingleMaintenancePart
    {
        public string VehiclePartName { get; set; }

        public int Cost { get; set; }
    }
}
