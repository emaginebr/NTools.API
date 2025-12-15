using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTools.Domain.Services.Interfaces;
using System;

namespace BazzucaMedia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("{bucketName}/getFileUrl/{fileName}")]
        public ActionResult<string> GetFileUrl(string bucketName, string fileName)
        {
            try
            {
                _logger.LogInformation("Get File Url BucketName:{BucketName}, Filename: {FileName}", bucketName, fileName);
                var url = _fileService.GetFileUrl(bucketName, fileName);
                _logger.LogInformation("Returned URL: {Url}", url);
                return Ok(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting file URL. Exception: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [RequestSizeLimit(100_000_000)]
        [HttpPost("{bucketName}/uploadFile")]
        public ActionResult<string> UploadFile(string bucketName, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogError("No file uploaded");
                    return BadRequest("No file uploaded");
                }
                _logger.LogInformation("Upload file, BucketName:{BucketName}, Filename: {FileName}, Size: {Size}", bucketName, file.FileName, file.Length);
                var fileName = _fileService.InsertFromStream(file.OpenReadStream(), bucketName, file.FileName);
                _logger.LogInformation("File name: {FileName}", fileName);
                return Ok(fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading file. Exception: {Message}", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}
