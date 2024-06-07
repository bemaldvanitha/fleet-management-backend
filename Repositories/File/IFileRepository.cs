using fleet_management_backend.Models.DTO.File;

namespace fleet_management_backend.Repositories.File
{
    public interface IFileRepository
    {
        public Task<PresignedUrlResponseDTO> GeneratePresignedUrl(PresignedUrlRequestDTO presignedUrlRequest);
    }
}
