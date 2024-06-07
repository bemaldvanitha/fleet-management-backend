using fleet_management_backend.Models.DTO.File;
using fleet_management_backend.Repositories.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fleet_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            this._fileRepository = fileRepository;
        }

        [HttpPost]
        [Route("Generate")]
        [Authorize]
        public async Task<IActionResult> GeneratePresignedUrl([FromBody] PresignedUrlRequestDTO presignedUrlRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                PresignedUrlResponseDTO presignedUrlResponse = await _fileRepository.GeneratePresignedUrl(presignedUrlRequest);

                if(presignedUrlResponse.StatusCode == 500)
                {
                    return BadRequest(presignedUrlResponse);
                }

                return Ok(presignedUrlResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
