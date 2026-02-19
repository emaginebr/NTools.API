using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using zTools.ACL.Interfaces;
using zTools.DTO.Settings;

namespace zTools.ACL
{
    public class DocumentClient : IDocumentClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<zToolsetting> _zToolsetting;
        private readonly ILogger<DocumentClient> _logger;

        public DocumentClient(HttpClient httpClient, IOptions<zToolsetting> zToolsetting, ILogger<DocumentClient> logger)
        {
            _httpClient = httpClient;
            _zToolsetting = zToolsetting;
            _logger = logger;
        }

        public async Task<bool> validarCpfOuCnpjAsync(string cpfCnpj)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/Document/validarCpfOuCnpj/{cpfCnpj}";
            _logger.LogInformation("Accessing URL: {Url}", url);
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            
            _logger.LogInformation("Response received: {Response}", json);
            
            return JsonConvert.DeserializeObject<bool>(json);
        }
    }
}
