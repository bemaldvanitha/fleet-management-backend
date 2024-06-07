namespace fleet_management_backend.Models.DTO.File
{
    public class PresignedUrlRequestDTO
    {
        public string FileName { get; set; }

        public string FileExtention { get; set; }

        public string Destination { get; set; }
    }
}
