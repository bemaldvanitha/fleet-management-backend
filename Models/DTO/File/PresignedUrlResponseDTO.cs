namespace fleet_management_backend.Models.DTO.File
{
    public class PresignedUrlResponseDTO
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public PresignedUrlResponse? urlResponse { get; set; }
    }

    public class PresignedUrlResponse
    {
        public string PreSignedUrl { get; set; }

        public string NewFileName { get; set; }
    }
}
