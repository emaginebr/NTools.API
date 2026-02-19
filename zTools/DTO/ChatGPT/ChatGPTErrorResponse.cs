using Newtonsoft.Json;

namespace zTools.DTO.ChatGPT
{
    public class ChatGPTErrorResponse
    {
        [JsonProperty("error")]
        public ChatGPTError Error { get; set; }
    }
}
