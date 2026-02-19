using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using zTools.ACL.Interfaces;
using zTools.DTO.ChatGPT;
using zTools.DTO.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace zTools.ACL
{
    public class ChatGPTClient : IChatGPTClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<zToolsetting> _zToolsetting;
        private readonly ILogger<ChatGPTClient> _logger;

        public ChatGPTClient(HttpClient httpClient, IOptions<zToolsetting> zToolsetting, ILogger<ChatGPTClient> logger)
        {
            _httpClient = httpClient;
            _zToolsetting = zToolsetting;
            _logger = logger;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/ChatGPT/sendMessage";
            _logger.LogInformation("Sending message to ChatGPT via URL: {Url}", url);

            var request = new ChatGPTMessageRequest
            {
                Message = message
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(request), 
                Encoding.UTF8, 
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            
            var aiResponse = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("ChatGPT message response received");

            return aiResponse;
        }

        public async Task<string> SendConversationAsync(List<ChatMessage> messages)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/ChatGPT/sendConversation";
            _logger.LogInformation("Sending conversation to ChatGPT via URL: {Url} with {Count} messages", url, messages.Count);

            var content = new StringContent(
                JsonConvert.SerializeObject(messages), 
                Encoding.UTF8, 
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            
            var aiResponse = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("ChatGPT conversation response received");

            return aiResponse;
        }

        public async Task<ChatGPTResponse> SendRequestAsync(ChatGPTRequest request)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/ChatGPT/sendRequest";
            _logger.LogInformation("Sending custom request to ChatGPT via URL: {Url}", url);

            var content = new StringContent(
                JsonConvert.SerializeObject(request), 
                Encoding.UTF8, 
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("ChatGPT custom request response received");

            return JsonConvert.DeserializeObject<ChatGPTResponse>(json);
        }

        public async Task<DallEResponse> GenerateImageAsync(string prompt)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/ChatGPT/generateImage";
            _logger.LogInformation("Generating image with DALL-E via URL: {Url}", url);

            var request = new ChatGPTMessageRequest
            {
                Message = prompt
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(request), 
                Encoding.UTF8, 
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Image generation response received");

            return JsonConvert.DeserializeObject<DallEResponse>(json);
        }

        public async Task<DallEResponse> GenerateImageAdvancedAsync(DallERequest request)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/ChatGPT/generateImageAdvanced";
            _logger.LogInformation("Generating image with DALL-E (advanced) via URL: {Url}", url);

            var content = new StringContent(
                JsonConvert.SerializeObject(request), 
                Encoding.UTF8, 
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Image generation response received");

            return JsonConvert.DeserializeObject<DallEResponse>(json);
        }
    }
}
