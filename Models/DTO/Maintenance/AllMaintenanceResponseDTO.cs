namespace fleet_management_backend.Models.DTO.Maintenance
{
    public class AllMaintenanceResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public List<MaintenanceObject>? Maintenances { get; set; }
    }

    public class MaintenanceObject
    {
        public Guid Id { get; set; }

        public Guid VehicleId { get; set; }

        public string MaintenanceType { get; set; }

        public int TotalCost { get; set; }

        public bool IsRegular { get; set; }

        public List<MaintenancePartObject> MaintenanceParts { get; set; }
    }

    public class MaintenancePartObject
    {
        public string PartName { get; set; }

        public int Cost { get; set; }
    }
}
