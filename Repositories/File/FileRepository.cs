using Amazon.S3;
using Amazon.S3.Model;
using fleet_management_backend.Data;
using fleet_management_backend.Models.DTO.File;

namespace fleet_management_backend.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly FleetManagerDbContext _context;
        private readonly IAmazonS3 _amazonS3;

        public FileRepository(FleetManagerDbContext context, IAmazonS3 amazonS3)
        {
            this._context = context;
            this._amazonS3 = amazonS3;
        }

        public async Task<PresignedUrlResponseDTO> GeneratePresignedUrl(PresignedUrlRequestDTO presignedUrlRequest)
        {
            try
            {
                string bucketName = "myfleetmanager";
                string objectKey = Guid.NewGuid().ToString() + presignedUrlRequest.FileExtention;

                DateTime expiration = DateTime.Now.AddHours(1);

                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = presignedUrlRequest.Destination + "/" + objectKey,
                    Verb = HttpVerb.PUT,
                    Expires = expiration,
                };

                string url = _amazonS3.GetPreSignedURL(request);

                return new PresignedUrlResponseDTO
                {
                    Message = "Presigned Url Generated",
                    StatusCode = 200,
                    urlResponse = new PresignedUrlResponse
                    {
                        NewFileName = objectKey,
                        PreSignedUrl = url
                    }
                };
            }
            catch (Exception ex)
            {
                return new PresignedUrlResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
