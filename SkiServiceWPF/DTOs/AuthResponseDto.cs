using Newtonsoft.Json;

namespace SkiServiceWPF.DTOs
{
    /// <summary>
    /// Data Transfer Object for authentication response
    /// </summary>
    public class AuthResponseDto
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
