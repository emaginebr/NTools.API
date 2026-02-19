using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zTools.ACL.Interfaces;
using zTools.DTO.Settings;

namespace zTools.ACL
{
    public class FileClient : IFileClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<zToolsetting> _zToolsetting;
        private readonly ILogger<FileClient> _logger;

        public FileClient(HttpClient httpClient, IOptions<zToolsetting> zToolsetting, ILogger<FileClient> logger)
        {
            _httpClient = httpClient;
            _zToolsetting = zToolsetting;
            _logger = logger;
        }

        public async Task<string> GetFileUrlAsync(string bucketName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }
            var url = $"{_zToolsetting.Value.ApiUrl}/File/{bucketName}/getFileUrl/{fileName}";
            _logger.LogInformation("Accessing URL: {Url}", url);
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            
            _logger.LogInformation("Response received: {Response}", json);
            
            return json ?? string.Empty;
        }

        public async Task<string> UploadFileAsync(string bucketName, IFormFile file)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/File/{bucketName}/uploadFile";
            _logger.LogInformation("Uploading file to URL: {Url}, FileName: {FileName}, ContentType: {ContentType}", url, file.FileName, file.ContentType);
            
            using (var formData = new MultipartFormDataContent())
            {
                using (var fileStream = file.OpenReadStream())
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    formData.Add(fileContent, "file", file.FileName);
                    var response = await _httpClient.PostAsync(url, formData);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    
                    _logger.LogInformation("Upload response received: {Response}", json);
                    
                    return json ?? string.Empty;
                }
            }
        }
    }
}
