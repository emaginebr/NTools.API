using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTools.Domain.Interfaces.Services;
using NTools.DTO.Domain;
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
        public ActionResult<StringResult> GetFileUrl(string bucketName, string fileName)
        {
            try
            {
                _logger.LogInformation("Get File Url BucketName:{@bucketName}, Filename: {@fileName}", bucketName, fileName);
                var url = _fileService.GetFileUrl(bucketName, fileName);
                _logger.LogInformation("Returned URL: {@url}", url);
                return new StringResult()
                {
                    Value = url
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [RequestSizeLimit(100_000_000)]
        [HttpPost("{bucketName}/uploadFile")]
        public ActionResult<StringResult> UploadFile(string bucketName, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogError("No file uploaded");
                    return BadRequest("No file uploaded");
                }
                _logger.LogInformation("Upload file, BucketName:{0}, Filename: {1}, Size: {2}", bucketName, file.FileName, file.Length);
                var fileName = _fileService.InsertFromStream(file.OpenReadStream(), bucketName, file.FileName);
                _logger.LogInformation("File name: {@fileName}", fileName);
                return new StringResult()
                {
                    Value = fileName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}
